using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.MediaStorage;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Models.Users;
using ClawLibrary.Core.Services;
using ClawLibrary.Services.Models.Users;
using Microsoft.Extensions.Logging;

namespace ClawLibrary.Services.ApiServices
{
    public class UsersApiService : BaseApiService, IUsersApiService
    {
        private readonly IUsersDataService _dataService;
        private readonly IMediaStorageAppService _mediaStorageAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersApiService> _logger;

        public UsersApiService(ISessionContextProvider provider, IUsersDataService dataService, IMediaStorageAppService mediaStorageAppService, IMapper mapper, ILogger<UsersApiService> logger) : base(provider)
        {
            _dataService = dataService;
            _mediaStorageAppService = mediaStorageAppService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserResponse> GetAuthenticatedUser()
        {
            if (Session != null && !string.IsNullOrWhiteSpace(Session.UserId))
            {
                var userKey = Session.UserId;

                _logger.LogInformation(
                    $"GetUserById input - userKey: {userKey}");

                User user = await _dataService.GetUserByKey(userKey);

                if (user == null)
                    throw new BusinessException(ErrorCode.UserDoesNotExist, $"User with key {userKey} does not exists!");

                _logger.LogInformation($"GetUserById response - user: {user}");

                return _mapper.Map<User, UserResponse>(user);
            }
            throw new UnauthorizedAccessException();
        }

        public async Task<UserResponse> GetUserByKey(string userKey)
        {
            _logger.LogInformation(
                $"GetUserById input - userKey: {userKey}");

            User user = await _dataService.GetUserByKey(userKey);

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist, $"User with key {userKey} does not exists!");

            _logger.LogInformation($"GetUserById response - user: {user}");

            return _mapper.Map<User, UserResponse>(user);
        }

        public async Task<ListResponse<UserResponse>> GetUsers(QueryData query)
        {
            if (Session != null && !string.IsNullOrWhiteSpace(Session.UserId))
            {
                var userKey = Session.UserId;
                _logger.LogInformation(
                    $"GetUsers input - query: {query}");

                ListResponse<User> users = await _dataService.GetUsers(userKey, query.Count, query.Offset,
                    query.OrderBy, query.SearchString);

                _logger.LogInformation($"GetUsers response - users: {users.TotalCount}");

                return _mapper.Map<ListResponse<User>, ListResponse<UserResponse>>(users);
            }
            throw new UnauthorizedAccessException();
        }

        public async Task<UserResponse> UpdateUser(UserRequest model)
        {
            if (Session != null && !string.IsNullOrWhiteSpace(Session.UserId))
            {
                var userId = Session.UserId;
                _logger.LogInformation(
                    $"UpdateUser input - model: {model}, userId: {userId}");
                var dto = _mapper.Map<UserRequest, User>(model);
                User user = await _dataService.Update(dto, userId);
                _logger.LogInformation($"GetUserById response - user: {user}");
                return _mapper.Map<User, UserResponse>(user);
            }
            throw new UnauthorizedAccessException();
        }

        public async Task<UserResponse> UpdateUserStatus(string userKey, Status status)
        {
            if (Session != null && !string.IsNullOrWhiteSpace(Session.UserId))
            {
                var modifiedByKey = Session.UserId;
                _logger.LogInformation($"UpdateUserStatus input - userKey: {userKey}, status: {status}, modifiedByKey: {modifiedByKey}");
                User user = await _dataService.UpdateStatus(userKey, status, modifiedByKey);
                _logger.LogInformation($"UpdateUserStatus response - user: {user}");
                return _mapper.Map<User, UserResponse>(user);
            }
            throw new UnauthorizedAccessException();
        }

        public Task<Media> UpdatePicture(string requestPictureBase64)
        {
            if (Session != null && !string.IsNullOrWhiteSpace(Session.UserId))
            {
                var userId = Session.UserId;
                _logger.LogInformation(
                    $"UpdateOrgPicture input - userId: {userId}");

                var bytes = Convert.FromBase64String(requestPictureBase64);
                ValidatePhoto(bytes);
                string fileName = _mediaStorageAppService.SaveMedia(bytes);
                _dataService.UpdatePicture(fileName, userId);
                return GetPicture();
            }
            throw new UnauthorizedAccessException();
        }

        public async Task<Media> GetPicture()
        {
            if (Session != null && !string.IsNullOrWhiteSpace(Session.UserId))
            {
                var userId = Session.UserId;
                _logger.LogInformation(
                    $"GetUserPicture input - userId: {userId}");

                string fileName = await _dataService.GetPicture(userId);
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
            throw new UnauthorizedAccessException();
        }

        public Task<Media> UpdatePicture(string requestPictureBase64, string userKey)
        {
            _logger.LogInformation(
                $"UpdateOrgPicture input - userKey: {userKey}");

            var bytes = Convert.FromBase64String(requestPictureBase64);
            ValidatePhoto(bytes);
            string fileName = _mediaStorageAppService.SaveMedia(bytes);
            _dataService.UpdatePicture(fileName, userKey);
            return GetPicture();
        }

        public async Task<Media> GetPicture(string userKey)
        {
            _logger.LogInformation(
                $"GetUserPicture input - userKey: {userKey}");

            string fileName = await _dataService.GetPicture(userKey);
            var content = _mediaStorageAppService.GetMedia(fileName);

            var media = new Media()
            {
                Content = content,
                FileName = fileName,
                ContentSize = content.Length,
                Id = fileName
            };

            _logger.LogInformation(
                $"GetUserPicture input - media: {media}, userKey: {userKey}");

            return media;
        }

        private void ValidatePhoto(byte[] bytes)
        {
            if (bytes.LongLength > 3000000)
            {
                throw new BusinessException(ErrorCode.FileSizeIsTooBig);
            }
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                    Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                throw new BusinessException(ErrorCode.WrongImageFile);
            }
        }
    }
}
