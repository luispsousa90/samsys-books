namespace BooksAPI2.Infrastructure.Models.Book;

public class BookDto
{
    public Guid Id { get; set; }
    public int Isbn { get; set; }
    public string Name { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public decimal Price { get; set; }
    public string AuthorName { get; set; }
}