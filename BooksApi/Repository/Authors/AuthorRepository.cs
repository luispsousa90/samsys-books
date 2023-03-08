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

    public IOrderedQueryable<Author> GetAllAuthors()
    {
        return FindAll().OrderBy(author => author.Name);
    }

    public IQueryable<Author> GetAuthorById(long authorId)
    {
        return FindByCondition(author => author.Id == authorId);
    }

    public IQueryable<Author> GetAuthorWithDetails(long authorId)
    {
        return FindByCondition(author => author.Id == authorId).Include(b => b.Books);
    }

    public IQueryable<Author> CreateAuthor(Author author)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Author> UpdateAuthor(long id, Author author)
    {
        throw new NotImplementedException();
    }
}