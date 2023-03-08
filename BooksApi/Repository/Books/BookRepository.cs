using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Repository.Shared;

namespace BooksApi.Repository.Books
{
  public class BookRepository : Repository<Book>, IBookRepository
  {
    public BookRepository(RepositoryContext repositoryContext)
      : base(repositoryContext)
    {
    }
    //public IQueryable<Book> GetBooksByAuthor(long authorId)
    //{
    //  return FindByCondition(book => book.AuthorId.Equals(authorId));
    //}
  }
}