namespace BooksAPI2.Infrastructure.Helpers;

public class BookParameters : QueryStringParameters
{
    public BookParameters()
    {
        OrderBy = "name";
    }

    public int Isbn { get; set; }
    public string? Name { get; set; }
    public Guid AuthorId { get; set; }
}