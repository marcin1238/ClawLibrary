using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.MediaStorage;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Models.Authors;
using ClawLibrary.Core.Models.Books;
using ClawLibrary.Core.Models.Categories;
using ClawLibrary.Core.Services;
using ClawLibrary.Services.Models.Books;
using Microsoft.Extensions.Logging;

namespace ClawLibrary.Services.ApiServices
{
    public class BooksApiService : BaseApiService, IBooksApiService
    {
        private readonly IBooksDataService _dataService;
        private readonly IMediaStorageAppService _mediaStorageAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksApiService> _logger;

        public BooksApiService(ISessionContextProvider provider, IBooksDataService dataService,
            IMediaStorageAppService mediaStorageAppService, ILogger<BooksApiService> logger,
            IMapper mapper) : base(provider)
        {
            _dataService = dataService;
            _mediaStorageAppService = mediaStorageAppService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BookResponse> GetBookByKey(string bookKey)
        {
            _logger.LogInformation(
                $"GetBookByKey input - bookKey: {bookKey}");

            Book book = await _dataService.GetBookByKey(bookKey);

            if (book == null)
                throw new BusinessException(ErrorCode.BookDoesNotExist, $"Book with key {bookKey} does not exists!");

            _logger.LogInformation($"GetBookByKey response - book: {book}");

            return _mapper.Map<Book, BookResponse>(book);
        }

        public async Task<BookResponse> CreateBook(BookRequest model)
        {
            var user = Session.UserEmail;
            _logger.LogInformation(
                $"CreateBook input - model: {model}");
            var dto = _mapper.Map<BookRequest, Book>(model);
            dto.Author = new Author() { Key = model.AuthorKey };
            dto.Category = new Category() { Key = model.CategoryKey };
            dto.CreatedBy = user;
            Book book = await _dataService.CreateBook(dto);

            if (book == null)
                throw new BusinessException(ErrorCode.InternalError, "Book has not been created!");

            _logger.LogInformation($"CreateBook response - book: {book}");
            return _mapper.Map<Book, BookResponse>(book);
        }

        public async Task<ListResponse<BookResponse>> GetBooks(QueryData query)
        {
            _logger.LogInformation(
                $"GetBooks input - query: {query}");

            ListResponse<Book> books = await _dataService.GetBooks(query.Count, query.Offset, query.OrderBy, query.SearchString);

            _logger.LogInformation($"GetBooks response - users: {books.TotalCount}");

            return _mapper.Map<ListResponse<Book>, ListResponse<BookResponse>>(books);
        }

        public async Task<BookResponse> UpdateBook(string bookKey, BookUpdateRequest model)
        {
            var user = Session.UserEmail;
            _logger.LogInformation(
                $"UpdateBook input - model: {model}");
            var dto = _mapper.Map<BookUpdateRequest, Book>(model);
            dto.Key = bookKey;
            dto.Author = new Author() {Key = model.AuthorKey };
            dto.Category = new Category() { Key = model.CategoryKey };
            dto.ModifiedBy = user;
            Book book = await _dataService.UpdateBook(dto);
            _logger.LogInformation($"UpdateBook response - book: {book}");
            return _mapper.Map<Book, BookResponse>(book);
        }

        public async Task<Media> UpdatePicture(string requestPictureBase64, string bookKey)
        {
            var user = Session.UserEmail;
            _logger.LogInformation(
                $"UpdatePicture input - userKey: {bookKey}");

            var bytes = Convert.FromBase64String(requestPictureBase64);
            ValidatePhoto(bytes);
            string fileName = _mediaStorageAppService.SaveMedia(bytes);
            await _dataService.UpdatePicture(fileName, bookKey, user);
            return await GetPicture(bookKey);
        }

        public async Task<Media> GetPicture(string bookKey)
        {
           
            _logger.LogInformation(
                $"GetPicture input - bookKey: {bookKey}");

            string fileName = await _dataService.GetPicture(bookKey);
            var content = _mediaStorageAppService.GetMedia(fileName);

            var media = new Media()
            {
                Content = content,
                FileName = fileName,
                ContentSize = content.Length,
                Id = fileName
            };

            _logger.LogInformation(
                $"GetPicture input - media: {media}, bookKey: {bookKey}");

            return media;
        }

        private void ValidatePhoto(byte[] bytes)
        {
            if (bytes.LongLength > 3000000)
            {
                throw new BusinessException(ErrorCode.FileSizeIsToBig);
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
