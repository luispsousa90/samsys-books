namespace BooksAPI2.Infrastructure.Interfaces.Helpers;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}