using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Models;
using ClawLibrary.Services.Models.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClawLibrary.Web.Controllers
{
    /// <summary>
    /// Books controller. Provides functionality to the api/books/ route.
    /// </summary>
    [Route("api/books")]
    [Authorize(Roles = "Regular,Admin")]
    public class BooksController : Controller
    {
        private readonly IBooksApiService _apiService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksController"/> class.
        /// </summary>
        /// <param name="apiService"></param>
        public BooksController(IBooksApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: api/books/book/{bookKey}
        /// <summary>
        /// Gets a book with specified key (if one is present).
        /// </summary>
        /// <param name="bookKey">Key of the book</param>
        /// <returns>Book details</returns>
        [HttpGet]
        [Route("book/{bookKey}")]
        public async Task<IActionResult> Get(string bookKey)
        {
            var item = await _apiService.GetBookByKey(bookKey);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        // GET: api/books
        /// <summary>
        /// Gets a list of the books.
        /// </summary>
        /// <param name="query">Limit the rows returned to a specified range</param>
        /// <returns>List of books</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] QueryData query)
        {
            var item = await _apiService.GetBooks(query);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/books/
        /// <summary>
        /// Creates new book.
        /// </summary>
        /// <param name="model">Book data</param>
        /// <returns>Book details</returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] BookRequest model)
        {
            var item = await _apiService.CreateBook(model);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        // PUT api/books/book/{bookKey}
        /// <summary>
        /// Updates the book with specified key (if one is present).
        /// </summary>
        /// <param name="bookKey">Key of the book</param>
        /// <param name="model">Book data</param>
        /// <returns>Book details</returns>
        [HttpPut]
        [Route("book/{bookKey}")]
        public async Task<IActionResult> Put(string bookKey, [FromBody] BookUpdateRequest model)
        {
            var item = await _apiService.UpdateBook(bookKey, model);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // PATCH api/books/book/{bookKey}/picture
        /// <summary>
        /// Updates the book picture for authenticated user
        /// </summary>
        /// <param name="request">Picture base64 string</param>
        /// <param name="bookKey">Key of the book</param>
        /// <returns>Book picture</returns>
        [HttpPatch]
        [Route("book/{bookKey}/picture")]
        public async Task<IActionResult> Picture(string bookKey, [FromBody] UpdatePictureRequest request)
        {
            Media media = await _apiService.UpdatePicture(request.PictureBase64, bookKey);
            if (media == null)
            {
                return NotFound();
            }
            return File(media.Content, "image/jpeg");
        }

        // GET api/books/book/{bookKey}/picture
        /// <summary>
        /// Gets picture for book with specified key (if one is present).
        /// </summary>
        /// <returns>User picture</returns>
        [HttpGet]
        [Route("book/{bookKey}/picture")]
        public async Task<IActionResult> Picture(string bookKey)
        {
            Media media = await _apiService.GetPicture(bookKey);
            if (media == null)
            {
                return NotFound();
            }
            return File(media.Content, "image/jpeg");
        }
    }
}
