using BooksApi.Models.Authors;

namespace BooksApi.Repository.Authors;

public interface IAuthorRepository : IRepositoryBase<Author>
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<Author?> GetAuthorById(long authorId);
    Task<Author?> GetAuthorWithDetails(long authorId);
    void CreateAuthor(Author author);
    void UpdateAuthor(Author author);
    void DeleteAuthor(Author author);
}