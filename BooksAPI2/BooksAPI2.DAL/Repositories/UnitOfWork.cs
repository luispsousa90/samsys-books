using BooksAPI2.Infrastructure.Interfaces.Repositories;

namespace BooksAPI2.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IAuthorRepository? _author;
    private IBookRepository? _book;

    public IBookRepository Book
    {
        get
        {
            if (_book == null)
            {
                _book = new BookRepository(_context);
            }

            return _book;
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IAuthorRepository Author
    {
        get
        {
            if (_author == null)
            {
                _author = new AuthorRepository(_context);
            }

            return _author;
        }
    }

    public UnitOfWork(ApplicationDbContext repositoryContext)
    {
        _context = repositoryContext;
    }
}