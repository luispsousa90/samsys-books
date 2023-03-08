using BooksApi.Data;
using BooksApi.Models.Authors;
using BooksApi.Repository.Shared;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repository.Authors;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await FindAll().OrderBy(author => author.Name).ToListAsync();
    }

    public async Task<Author> GetAuthorById(long authorId)
    {
        return await FindByCondition(author => author.Id == authorId).FirstOrDefaultAsync();
    }

    public async Task<Author> GetAuthorWithDetails(long authorId)
    {
        return await FindByCondition(author => author.Id == authorId).Include(b => b.Books).FirstOrDefaultAsync();
    }

    public void CreateAuthor(Author author)
    {
        Create(author);
    }

    public void UpdateAuthor(long id, Author author)
    {
        Update(author);
    }
}