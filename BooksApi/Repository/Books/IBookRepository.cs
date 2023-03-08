using BooksApi.Models;

namespace BooksApi.Repository.Books
{
  public interface IBookRepository : IRepositoryBase<Book>
  {
    //IQueryable<Book> GetBooksByAuthor(long authorId);
  }
}