namespace BooksApi.Models.Books;

public class BookDto
{
    public long Id { get; set; }
    public int Isbn { get; set; }
    public string Name { get; set; } = null!;
    public long AuthorId { get; set; }
    public decimal Price { get; set; }
}