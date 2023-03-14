namespace BooksAPI2.Infrastructure.Interfaces.Repositories;

public interface IUnitOfWork
{
    IAuthorRepository Author { get; }
    IBookRepository Book { get; }
    Task SaveAsync();
}