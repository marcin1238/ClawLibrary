using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Services.Models.Books;

namespace ClawLibrary.Services.ApiServices
{
    /// <summary>
    /// Books api service
    /// </summary>
    public interface IBooksApiService
    {
        /// <summary>
        /// Gets the book with specified key (if one is present).
        /// </summary>
        /// <param name="bookKey"></param>
        /// <returns>Book details response</returns>
        Task<BookResponse> GetBookByKey(string bookKey);

        /// <summary>
        /// Creates new book.
        /// </summary>
        /// <param name="model">Book model</param>
        /// <returns>Book details response</returns>
        Task<BookResponse> CreateBook(BookRequest model);

        /// <summary>
        /// Get a list of the books.
        /// </summary>
        /// <param name="query"> Limit the rows returned to a specified range</param>
        /// <returns>List of books response</returns>
        Task<ListResponse<BookResponse>> GetBooks(QueryData query);

        /// <summary>
        /// Updates the book with specified key (if one is present).
        /// </summary>
        /// <param name="bookKey">Key of the book</param>
        /// <param name="model">Book model</param>
        /// <returns>User details response</returns>
        Task<BookResponse> UpdateBook(string bookKey, BookRequest model);

        /// <summary>
        /// Updates the book picture.
        /// </summary>
        /// <param name="requestPictureBase64">Picture base64 string</param>
        ///  <param name="bookKey">Key of the book</param>
        /// <returns>Book picture response</returns>
        Task<Media> UpdatePicture(string requestPictureBase64, string bookKey);

        /// <summary>
        /// Gets picture for book with specified key (if one is present).
        /// </summary>
        /// <param name="bookKey">Key of the book</param>
        /// <returns>Book picture response</returns>
        Task<Media> GetPicture(string bookKey);
    }
}