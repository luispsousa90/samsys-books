namespace BooksApi.Repository.Shared;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}