using System.Collections.Generic;
using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Models.Books;

namespace ClawLibrary.Core.DataServices
{
    /// <summary>
    /// Books data service
    /// </summary>
    public interface IBooksDataService
    {
        /// <summary>
        /// Gets the book with specified key (if one is present).
        /// </summary>
        /// <returns>Book details</returns>
        Task<Book> GetBookByKey(string bookKey);

        /// <summary>
        /// Creates new book.
        /// </summary>
        /// <param name="model">Book model</param>
        /// <returns>Book details</returns>
        Task<Book> CreateBook(Book model);

        /// <summary>
        /// Get a list of the books.
        /// </summary>
        /// <param name="count">Limit the rows returned to a specified range</param>
        /// <param name="offset">Offset of rows returned</param>
        /// <param name="orderBy">Order the result set of a query by the specified column</param>
        /// <param name="searchString">User first name or last name or email</param>
        /// <returns>List of books</returns>
        Task<ListResponse<Book>> GetBooks(int? count, int? offset, string orderBy, string searchString);

        /// <summary>
        /// Updates the book with specified key (if one is present).
        /// </summary>
        /// <param name="model">Book model</param>
        /// <returns>Book details</returns>
        Task<Book> UpdateBook(Book model);

        /// <summary>
        /// Updates the book picture.
        /// </summary>
        /// <param name="fileName">Picture file name</param>
        /// <param name="bookKey">Key of the book</param>
        /// <param name="modifiedBy"></param>
        /// <returns>Book picture</returns>
        Task UpdatePicture(string fileName, string bookKey, string modifiedBy);

        /// <summary>
        /// Gets file name of picture for book with specified key (if one is present)..
        /// </summary>
        /// <param name="bookKey">Key of the book</param>
        /// <returns>Book picture file name</returns>
        Task<string> GetFileNameOfBookPicture(string bookKey);
    }
}