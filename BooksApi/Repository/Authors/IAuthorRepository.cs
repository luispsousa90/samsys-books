using BooksApi.Models.Authors;

namespace BooksApi.Repository.Authors;

public interface IAuthorRepository : IRepositoryBase<Author>
{
    IOrderedQueryable<Author> GetAllAuthors();
    IQueryable<Author> GetAuthorById(long authorId);
    IQueryable<Author> GetAuthorWithDetails(long authorId);
    IQueryable<Author> CreateAuthor(Author author);
    IQueryable<Author> UpdateAuthor(long id, Author author);
}