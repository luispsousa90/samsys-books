namespace BooksAPI2.Infrastructure.Models.Book;

public class BookDto
{
    public Guid Id { get; set; }
    public int Isbn { get; set; }
    public string Name { get; set; }
    public Guid AuthorId { get; set; }
    public decimal Price { get; set; }
    public string AuthorName { get; set; }

    public BookDto()
    {
    }
    public BookDto(Entities.Book book)
    {
        Id = book.Id;
        Isbn = book.Isbn;
        Name = book.Name;
        AuthorId = book.AuthorId;
        Price = book.Price;
        AuthorName = book.Author.Name;
    }
}