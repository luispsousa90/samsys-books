using BooksApi.Helpers;
using BooksApi.Models.Books;

namespace BooksApi.Repository.Books
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<PagedList<Book>> GetAllBooks(BookParameters bookParameters);
        Task<Book?> GetBookById(long bookId);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void SoftDeleteBook(Book book);
    }
}