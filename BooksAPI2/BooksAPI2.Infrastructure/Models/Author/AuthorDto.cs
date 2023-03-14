using BooksAPI2.Infrastructure.Models.Book;

namespace BooksAPI2.Infrastructure.Models.Author;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<BookDto> Books { get; set; }
}