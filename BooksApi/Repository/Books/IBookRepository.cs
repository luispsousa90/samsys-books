using BooksApi.Models;
using BooksApi.Models.Books;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Repository.Books
{
  public interface IBookRepository : IRepositoryBase<Book>
  {
    IOrderedQueryable<Book> GetAllBooks();
    IQueryable<Book> GetBookById(long bookId);
    IQueryable<Book> CreateBook(Book book);
    IQueryable<Book> UpdateBook(long id, Book book);
    Task<IActionResult> DeleteBook(long id);
  }
}