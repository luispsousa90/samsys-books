using System.ComponentModel.DataAnnotations;
using BooksApi.Models.Books;

namespace BooksApi.Models.Authors;

public class Author
{
    public long Id { get; set; }
    [Required] public string Name { get; set; } = null!;

    public ICollection<Book> Books { get; set; }
}