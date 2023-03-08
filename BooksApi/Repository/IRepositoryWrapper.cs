using BooksApi.Repository.Authors;
using BooksApi.Repository.Books;

namespace BooksApi.Repository
{
  public interface IRepositoryWrapper
  {
    IAuthorRepository Author { get; }
    IBookRepository Book { get; }
    void Save();
  }
}