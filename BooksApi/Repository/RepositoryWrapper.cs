using BooksApi.Data;
using BooksApi.Repository.Authors;
using BooksApi.Repository.Books;

namespace BooksApi.Repository;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly RepositoryContext _repoContext;
    private IAuthorRepository _author;
    private IBookRepository _book;

    public IBookRepository Book
    {
        get
        {
            if (_book == null)
            {
                _book = new BookRepository(_repoContext);
            }
            return _book;
        }
    }

    public IAuthorRepository Author
    {
        get
        {
            if (_author == null)
            {
                _author = new AuthorRepository(_repoContext);
            }
            return _author;
        }
    }

    public RepositoryWrapper(RepositoryContext repositoryContext)
    {
        _repoContext = repositoryContext;
    }

    public void Save()
    {
        _repoContext.SaveChanges();
    }
}
