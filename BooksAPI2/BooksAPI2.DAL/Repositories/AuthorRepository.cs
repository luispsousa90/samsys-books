using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI2.DAL.Repositories;

public class AuthorRepository: GenericRepository<Author>, IAuthorRepository
{
    public AuthorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await FindAll().OrderBy(author => author.Name).ToListAsync();
    }

    public async Task<Author?> GetAuthorById(Guid authorId)
    {
        return await FindByCondition(author => author.Id.Equals(authorId)).FirstOrDefaultAsync();
    }

    public async Task<Author?> GetAuthorWithDetails(Guid authorId)
    {
        return await FindByCondition(author => author.Id.Equals(authorId)).Include(b => b.Books).FirstOrDefaultAsync();
    }

    public void CreateAuthor(Author author)
    {
        Create(author);
    }

    public void UpdateAuthor(Author author)
    {
        Update(author);
    }

    public void DeleteAuthor(Author author)
    {
        Delete(author);
    }
}