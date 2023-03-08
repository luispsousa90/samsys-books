using BooksApi.Models.Books;

namespace BooksApi.Models.Authors;

public class AuthorDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<BookDto> Books { get; set; }
}