using BooksApi.Helpers;

namespace BooksApi.Models.Books;

public class BookParameters : QueryStringParameters
{
    public BookParameters()
    {
        OrderBy = "name";
    }

    public int Isbn { get; set; }
    public string? Name { get; set; }
    public long AuthorId { get; set; }
}