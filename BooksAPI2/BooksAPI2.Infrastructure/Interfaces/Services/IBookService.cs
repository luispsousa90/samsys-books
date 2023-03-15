using BooksAPI2.Infrastructure.Helpers;
using BooksAPI2.Infrastructure.Models.Book;

namespace BooksAPI2.Infrastructure.Interfaces.Services;

public interface IBookService
{
    Task<MessagingHelper<PagedList<BookDto>>> GetAllBooks(BookParameters bookParameters);

    Task<MessagingHelper<BookDto>> GetBookById(Guid bookId);

    Task<MessagingHelper> CreateBook(BookForCreationDto createBookDto);

    Task<MessagingHelper> UpdateBook(Guid id, BookForUpdateDto book);

    Task<MessagingHelper> DeleteBook(Guid id);

    Task<MessagingHelper> SoftDeleteBook(Guid id);
}