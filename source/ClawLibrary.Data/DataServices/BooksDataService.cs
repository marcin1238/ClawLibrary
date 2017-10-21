using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.Models;
using ClawLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using Book = ClawLibrary.Core.Models.Books.Book;

namespace ClawLibrary.Data.DataServices
{
    public class BooksDataService : IBooksDataService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public BooksDataService(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Book> GetBookByKey(string bookKey)
        {
            var book = await _context.Book
                .FirstAsync(x => x.Key.ToString().ToLower().Equals(bookKey.ToLower()) 
                && x.Status.ToLower().Equals(Status.Active.ToString().ToLower()));

            return _mapper.Map<ClawLibrary.Data.Models.Book, Book>(book);
        }

        public async Task<Book> CreateBook(Book model)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => (x.Title.ToString().ToLower().Equals(model.Title.ToLower()) ||
                                                           x.Isbn.ToString().ToLower().Equals(model.Isbn.ToLower())) &&
                                                           x.Status.ToLower().Equals(Status.Active.ToString().ToLower()));

            if (book == null)
            {
                var author = await _context.Author.FirstAsync(
                    x => x.Key.ToString().ToLower().Equals(model.Author.Key.ToString().ToLower()) &&
                         x.Status.ToLower().Equals(Status.Active.ToString().ToLower()));

                if (author == null)
                    throw new BusinessException(ErrorCode.AuthorDoesNotExist);

                var category = await _context.Category.FirstAsync(
                    x => x.Key.ToString().ToLower().Equals(model.Category.Key.ToString().ToLower()) &&
                         x.Status.ToLower().Equals(Status.Active.ToString().ToLower()));

                if (category == null)
                    throw new BusinessException(ErrorCode.CategoryDoesNotExist);

                book = _mapper.Map<Book, ClawLibrary.Data.Models.Book>(model);
                book.Key = Guid.NewGuid();
                book.Status = Status.Active.ToString();
                book.CreatedDate = DateTimeOffset.Now;
                book.Author = author;
                book.Category = category;

                var createdBook = await _context.Book.AddAsync(book);
                await _context.SaveChangesAsync();
                book = createdBook.Entity;
            }
            return _mapper.Map<ClawLibrary.Data.Models.Book, Book>(book);
        }

        public async Task<ListResponse<Book>> GetBooks(int? count, int? offset, string orderBy, string searchString)
        {
            long totalCount = await _context.Book.CountAsync(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()));
            List<Book> list;
            if (string.IsNullOrWhiteSpace(searchString))
                switch (orderBy.ToLower())
                {
                    case "title_asc":
                    {
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Title)
                            .ToListAsync());
                        break;
                    }
                    case "publisher_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Publisher)
                            .ToListAsync());
                        break;
                    case "language_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Language)
                            .ToListAsync());
                        break;
                    case "isbn_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Isbn)
                            .ToListAsync());
                        break;
                    case "description_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Description)
                            .ToListAsync());
                        break;
                    case "quantity_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync());
                        break;
                    case "paperback_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Paperback)
                            .ToListAsync());
                        break;
                    case "publishdate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.PublishDate)
                            .ToListAsync());
                        break;
                    case "createddate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.CreatedDate)
                            .ToListAsync());
                        break;
                    case "modifieddate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.ModifiedDate)
                            .ToListAsync());
                        break;
                    case "title_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Title)
                            .ToListAsync());
                        break;
                    case "publisher_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Publisher)
                            .ToListAsync());
                        break;
                    case "language_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Language)
                            .ToListAsync());
                        break;
                    case "isbn_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Isbn)
                            .ToListAsync());
                        break;
                    case "description_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Description)
                            .ToListAsync());
                        break;
                    case "quantity_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Quantity)
                            .ToListAsync());
                        break;
                    case "paperback_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Paperback)
                            .ToListAsync());
                        break;
                    case "publishdate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.PublishDate)
                            .ToListAsync());
                        break;
                    case "createddate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.CreatedDate)
                            .ToListAsync());
                        break;
                    case "modifieddate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.ModifiedDate)
                            .ToListAsync());
                        break;
                    default:
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .ToListAsync());
                        break;
                }
            else
                switch (orderBy.ToLower())
                {
                    case "title_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => x.Status.ToLower().Equals(Status.Active.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Title)
                            .ToListAsync());
                        break;
                    case "publisher_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                        x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                        x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Publisher)
                            .ToListAsync());
                        break;
                    case "language_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Language)
                            .ToListAsync());
                        break;
                    case "isbn_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Isbn)
                            .ToListAsync());
                        break;
                    case "description_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Description)
                            .ToListAsync());
                        break;
                    case "quantity_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync());
                        break;
                    case "paperback_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Paperback)
                            .ToListAsync());
                        break;
                    case "publishdate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.PublishDate)
                            .ToListAsync());
                        break;
                    case "createddate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.CreatedDate)
                            .ToListAsync());
                        break;
                    case "modifieddate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.ModifiedDate)
                            .ToListAsync());
                        break;
                    case "title_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Title)
                            .ToListAsync());
                        break;
                    case "publisher_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Publisher)
                            .ToListAsync());
                        break;
                    case "language_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Language)
                            .ToListAsync());
                        break;
                    case "isbn_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Isbn)
                            .ToListAsync());
                        break;
                    case "description_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Description)
                            .ToListAsync());
                        break;
                    case "quantity_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Quantity)
                            .ToListAsync());
                        break;
                    case "paperback_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Paperback)
                            .ToListAsync());
                        break;
                    case "publishdate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.PublishDate)
                            .ToListAsync());
                        break;
                    case "createddate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.CreatedDate)
                            .ToListAsync());
                        break;
                    case "modifieddate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.ModifiedDate)
                            .ToListAsync());
                        break;
                    default:
                        list = _mapper.Map<List<ClawLibrary.Data.Models.Book>, List<Book>>(await _context
                            .Book
                            .Include("Author")
                            .Include("Category")
                            .Where(x => (x.Status.ToLower().Equals(Status.Active.ToString().ToLower())) && (x.Title.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Isbn.ToLower().Contains(searchString.ToLower()) ||
                                                                                                            x.Publisher.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .ToListAsync());
                        break;
                }
            return new ListResponse<Book>(list, totalCount);

        }

        public async Task<Book> UpdateBook(Book model)
        {
            var book = await _context.Book.FirstAsync(x => (x.Key.ToString().ToLower().Equals(model.Key.ToLower())));

            if (book == null)
                throw new BusinessException(ErrorCode.BookDoesNotExist);

            var author = await _context.Author.FirstAsync(
                x => x.Key.ToString().ToLower().Equals(model.Author.Key.ToString().ToLower()) &&
                     x.Status.ToLower().Equals(Status.Active.ToString().ToLower()));

            if (author == null)
                throw new BusinessException(ErrorCode.AuthorDoesNotExist);

            var category = await _context.Category.FirstAsync(
                x => x.Key.ToString().ToLower().Equals(model.Category.Key.ToString().ToLower()) &&
                     x.Status.ToLower().Equals(Status.Active.ToString().ToLower()));

            if (category == null)
                throw new BusinessException(ErrorCode.CategoryDoesNotExist);

            book = _mapper.Map<Book, ClawLibrary.Data.Models.Book>(model);
            
            Status status;
            book.Status = Enum.TryParse(model.Status, true, out status) ? status.ToString() : throw new BusinessException(ErrorCode.WrongStatus);
            book.ModifiedDate = DateTimeOffset.Now;
            book.Author = author;
            book.Category = category;

            book = _context.Book.Update(book).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<ClawLibrary.Data.Models.Book, Book>(book);
        }

        public async Task UpdatePicture(string fileName, string bookKey, string modifiedBy)
        {
            File file;
            var book = await _context.Book.Include("ImageFile").FirstAsync(x => x.Key.ToString() == bookKey && x.Status == Status.Active.ToString());

            if (book == null)
                throw new BusinessException(ErrorCode.BookDoesNotExist);

            if (book.ImageFile == null)
            {
                file = new File();
                file.FileName = fileName;
                file.Key = Guid.NewGuid();
                file.CreatedBy = modifiedBy;
                file.CreatedDate = DateTimeOffset.Now;
                file.Status = Status.Active.ToString();
            }
            else
            {
                file = book.ImageFile;
                file.FileName = fileName;
                file.ModifiedBy = modifiedBy;
                file.ModifiedDate = DateTimeOffset.Now;
            }

            book.ImageFile = file;
            book.ModifiedBy = modifiedBy;
            book.ModifiedDate = DateTimeOffset.Now;

            _context.Book.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetPicture(string bookKey)
        {
            var user = await _context.Book.Include("ImageFile").FirstAsync(x => x.Key.ToString() == bookKey && x.Status == Status.Active.ToString());

            if (user == null)
                throw new BusinessException(ErrorCode.BookDoesNotExist);

            if (user.ImageFile == null)
                return string.Empty;
            return user.ImageFile.FileName;
        }
    }
}
