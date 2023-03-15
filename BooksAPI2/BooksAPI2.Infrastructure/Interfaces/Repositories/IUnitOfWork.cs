namespace BooksAPI2.Infrastructure.Interfaces.Repositories;

public interface IUnitOfWork
{
    IAuthorRepository AuthorRepository { get; }
    IBookRepository BookRepository { get; }
    Task SaveAsync();
}