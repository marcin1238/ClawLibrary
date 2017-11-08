using System;
using ClawLibrary.Core.Models.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ClawLibrary.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseClawLibraryAuth(this IApplicationBuilder app, Action<TokenProviderOptions> setupAction = null)
        {
            var tokenProviderOptions = app.ApplicationServices.GetService<TokenProviderOptions>() ?? new TokenProviderOptions();

            setupAction?.Invoke(tokenProviderOptions);

            return app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                Audience = tokenProviderOptions.Audience,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = tokenProviderOptions.SigningCredentials?.Key,

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = tokenProviderOptions.Issuer,

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = tokenProviderOptions.Audience,

                    // Validate the token expiry
                    ValidateLifetime = true,
                }
            });
        }
    }
}
