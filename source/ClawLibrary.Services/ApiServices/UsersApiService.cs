using System;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.MediaStorage;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Services;
using ClawLibrary.Services.Models.Users;
using Microsoft.Extensions.Logging;

namespace ClawLibrary.Services.ApiServices
{
    public class UsersApiService : BaseApiService, IUsersApiService
    {
        private readonly IUsersDataService _userDataService;
        private readonly IMediaStorageAppService _mediaStorageAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersApiService> _logger;

        public UsersApiService(ISessionContextProvider provider, IUsersDataService userDataService, IMediaStorageAppService mediaStorageAppService, IMapper mapper, ILogger<UsersApiService> logger) : base(provider)
        {
            _userDataService = userDataService;
            _mediaStorageAppService = mediaStorageAppService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserResponse> GetUserByKey()
        {
            var userKey = Session.UserId;
            _logger.LogInformation(
                $"GetUserById input - userKey: {userKey}");

            User user = await _userDataService.GetByKey(userKey);

            _logger.LogInformation($"GetUserById response - user: {user}");

            return _mapper.Map<User, UserResponse>(user);
        }

        public async Task<UserResponse> UpdateUser(UserRequest model)
        {
            var userId = Session.UserId;
            _logger.LogInformation(
                $"UpdateUser input - model: {model}, userId: {userId}");
            var dto = _mapper.Map<UserRequest, User>(model);
            User user = await _userDataService.Update(dto, userId);
            _logger.LogInformation($"GetUserById response - user: {user}");
            return _mapper.Map<User, UserResponse>(user);
        }

        public Task<Media> UpdatePicture(string requestPictureBase64)
        {
            var userId = Session.UserId;
            _logger.LogInformation(
                $"UpdateOrgPicture input - userId: {userId}");

            var bytes = Convert.FromBase64String(requestPictureBase64);
            ValidatePhoto(bytes);
            string fileName = _mediaStorageAppService.SaveMedia(bytes);
            _userDataService.UpdatePicture(fileName, userId);
            return GetPicture();
        }

        public async Task<Media> GetPicture()
        {
            var userId = Session.UserId;
            _logger.LogInformation(
                $"GetUserPicture input - userId: {userId}");

            string fileName = await _userDataService.GetPicture(userId);
            var content = _mediaStorageAppService.GetMedia(fileName);

            var media = new Media()
            {
                Content = content,
                FileName = fileName,
                ContentSize = content.Length,
                Id = fileName
            };

            _logger.LogInformation(
                $"GetUserPicture input - media: {media}, userId: {userId}");

            return media;
        }

        private void ValidatePhoto(byte[] bytes)
        {
            if (bytes.LongLength > 3000000)
            {
                throw new BusinessException(ErrorCode.FileSizeIsToBig);
            }
        }
    }
}
