using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.Models.Auth;
using ClawLibrary.Core.Models.Users;
using ClawLibrary.Mail;
using ClawLibrary.Mail.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using AuthorizeRequest = ClawLibrary.Services.Models.Users.AuthorizeRequest;
using RegisterUserRequest = ClawLibrary.Services.Models.Users.RegisterUserRequest;
using ResetUserPasswordRequest = ClawLibrary.Services.Models.Users.ResetUserPasswordRequest;
using SetUserPasswordRequest = ClawLibrary.Services.Models.Users.SetUserPasswordRequest;
using UserVerificationRequest = ClawLibrary.Services.Models.Users.UserVerificationRequest;

namespace ClawLibrary.Services.ApiServices
{
    public class AuthApiService : IAuthApiService
    {
        private readonly AuthConfig _config;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthDataService _dataService;
        private readonly IMapper _mapper;
        private readonly TokenProviderOptions _options;
        private readonly IMailGenerator _mailGenerator;
        private readonly IMailSender _mailSender;
        private readonly ILogger<AuthApiService> _logger;

        public AuthApiService(AuthConfig config, IAuthDataService dataService, IPasswordHasher passwordHasher,
            IMailGenerator mailGenerator,
            IMailSender mailSender,
            IMapper mapper,
            TokenProviderOptions options, ILogger<AuthApiService> logger)
        {
            _config = config;
            _dataService = dataService;
            _passwordHasher = passwordHasher;
            _options = options;
            _mailGenerator = mailGenerator;
            _mailSender = mailSender;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Token> Authorize(AuthorizeRequest user)
        {
            _logger.LogInformation($"Authorize user input - user: {user}");

            var token = await Authenticate(user);

            _logger.LogInformation($"Authorize user response - token: {token}");
            return token;
        }

        public async Task RegisterUser(RegisterUserRequest request)
        {
            _logger.LogInformation($"Register user input - request: {request}");
          
            var dto = _mapper.Map<RegisterUserRequest, User>(request);
            Guid salt = Guid.NewGuid();
            string hashedPassword = _passwordHasher.HashPassword(request.Password + salt);

            var user = await _dataService.RegisterUser(dto, hashedPassword, salt.ToString());

            _logger.LogInformation($"Register user - created user: {user}");
            
            var emailTask = _mailSender.SendAsync(_mailGenerator.PrepareEmail(new VerificationEmail()
            {
                ReceiverName = $"{user.FirstName} {user.LastName}",
                ReceiverEmail = user.Email,
                CustomerKey = user.Key
            }));
        }

        public async Task<Token> VerifyUser(UserVerificationRequest request)
        {
            _logger.LogInformation($"VerifyUser input - request: {request}");

            var user = await _dataService.GetUserByEmail(request.Email);

            if (user == null)
                throw new UnauthorizedAccessException();

            var saltedPassword = request.Password + user.Salt;
            var result = _passwordHasher.VerifyHashedPassword(user.Password, saltedPassword);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException();
            }

            if (!request.VerificationCode.ToLower().Equals(user.Key.ToLower()))
            {
                throw new BusinessException(ErrorCode.InvalidVerificationCode, "Invalid verification code");
            }
            if (!user.Status.ToString().ToLower().Equals(Status.Pending.ToString().ToLower()))
            {
                throw new BusinessException(ErrorCode.AccountAlreadyActivatedOrBlocked, "Account is already activated or blocked");
            }

            await _dataService.VerifyUser(user.Id);

            var token = await Authenticate(new AuthorizeRequest() { Email = request.Email, Password = request.Password });

            _logger.LogInformation($"VerifyUser response - token: {token}");

            return token;
        }

        public async Task ResetPassword(ResetUserPasswordRequest request)
        {
            var user = await _dataService.GetUserByEmail(request.Email);
            if (user != null)
            {
                var passwordResetKey = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                await _dataService.CreatePasswordResetKey(user.Id, passwordResetKey);

                var emailTask = _mailSender.SendAsync(_mailGenerator.PrepareEmail(new UserResetPasswordEmail()
                {
                    ReceiverName = $"{user.FirstName} {user.LastName}",
                    ReceiverEmail = user.Email,
                    PasswordResetKey = passwordResetKey
                }));
            }
            else
                throw new BusinessException(ErrorCode.UserDoesNotExist, $"User with email: {request.Email} does not exist!");
        }

        public async Task SetPassword(SetUserPasswordRequest request)
        {
            var user = await _dataService.GetUserByEmail(request.Email);

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist, $"User with email: {request.Email} does not exist!");

            if (user.PasswordResetKey != request.VerificationCode)
                throw new BusinessException(ErrorCode.InvalidVerificationCode, "Invalid verification code");

            var timeSpan = DateTimeOffset.Now - user.PasswordResetKeyCreatedDate;

            if (timeSpan == null || timeSpan.Value.TotalMinutes > _config.PasswordResetKeyExpirationInMinutes)
                throw new BusinessException(ErrorCode.PasswordResetKeyExpired, "Password reset key expired");
            
            Guid salt = Guid.NewGuid();
            string hashedPassword = _passwordHasher.HashPassword(request.Password + salt);
            await _dataService.ResetPassword(user.Id, hashedPassword, salt.ToString());
        }

        private async Task<Token> Authenticate(AuthorizeRequest request)
        {
            //Get user
            var user = await GetAuthenticatedUser(request.Email, request.Password);
            
            if (user == null || !user.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                throw new UnauthorizedAccessException();

            //Get Identity
            var identity = await GetIdentity(user);

            if (!IsIdentityValid(identity))
                throw new UnauthorizedAccessException();
            
            //Create token
            var expirationDate = DateTime.UtcNow.AddMinutes(_config.TokenExpirationInMinutes);
            var claims = MergeClaims(user.Email, expirationDate, identity);

            return CreateToken(expirationDate, user.Language, claims.ToArray());
        }

        private async Task<User> GetAuthenticatedUser(string username, string password)
        {
            var user = await _dataService.GetUserByEmail(username);
            if (user != null)
            {
                var saltedPassword = password + user.Salt;
                var result = _passwordHasher.VerifyHashedPassword(user.Password, saltedPassword);
                if (result == PasswordVerificationResult.Success)
                {
                    return user;
                }
            }
            return null;
        }

        private async Task<ClaimsIdentity> GetIdentity(User user)
        {
            if (user != null)
            {
                var roles = await _dataService.GetUserRoles(user.Id);
                var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

                claims.Add(new Claim(ClaimTypes.Sid, user.Key.ToString()));
                claims.Add(new Claim(ClaimTypes.GroupSid, user.Email.ToString()));

                return await Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(user.Email, "Token"), claims));
            }
            else
            {
                // Credentials are invalid, or account doesn't exist
                return await Task.FromResult<ClaimsIdentity>(null);
            }
        }

        private bool IsIdentityValid(ClaimsIdentity identity)
        {
            if (identity != null && identity.Claims.Any(claim =>
                    claim.Type == ClaimTypes.Role && (claim.Value.ToLower().Equals(Role.Admin.ToString().ToLower()) ||
                                                      claim.Value.ToLower().Equals(Role.Regular.ToString().ToLower()))))
                return true;
            return false;
        }

        private List<Claim> MergeClaims(string email, DateTime expirationDate, ClaimsIdentity identity)
        {
            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, (expirationDate - DateTime.UtcNow).TotalSeconds.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
            };

            foreach (var identityClaim in identity.Claims)
            {
                claims.Add(identityClaim);
            }

            return claims;
        }

        private Token CreateToken(DateTime expirationDate, string language, params Claim[] claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expirationDate,
                signingCredentials: _options.SigningCredentials);

            //Save user language preference into JWT
            jwt.Payload["language"] = language;

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new Token()
            {
                AccessToken = encodedJwt,
                ExpiresAt = expirationDate,
                ExpiresIn = (int)(expirationDate - DateTime.UtcNow).TotalSeconds,
                TokenType = "Bearer"
            };
        }
    }
}
