using BooksAPI2.Infrastructure.Entities;

namespace BooksAPI2.Infrastructure.Interfaces.Repositories;

public interface IAuthorRepository : IGenericRepository<Author>
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<Author?> GetAuthorById(Guid authorId);
    Task<Author?> GetAuthorWithDetails(Guid authorId);
    void CreateAuthor(Author author);
    void UpdateAuthor(Author author);
    void DeleteAuthor(Author author);
}