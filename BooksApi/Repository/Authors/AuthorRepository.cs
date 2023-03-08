using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Repository.Shared;

namespace BooksApi.Repository.Authors;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}