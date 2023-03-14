using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Helpers;

namespace BooksAPI2.Infrastructure.Interfaces.Repositories;

public interface IBookRepository: IGenericRepository<Book>
{
    Task<PagedList<Book>> GetAllBooks(BookParameters bookParameters);
    Task<Book?> GetBookById(Guid bookId);
    void CreateBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(Book book);
    void SoftDeleteBook(Book book);
}