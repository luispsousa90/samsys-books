using BooksApi.Models.Books;

namespace BooksApi.Repository.Books
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(long bookId);
        void CreateBook(Book book);
        void UpdateBook(long id, Book book);
        void DeleteBook(long id);
    }
}