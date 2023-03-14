using System.ComponentModel.DataAnnotations;

namespace BooksAPI2.Infrastructure.Entities;

public class Author
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public ICollection<Book> Books { get; set; }
}