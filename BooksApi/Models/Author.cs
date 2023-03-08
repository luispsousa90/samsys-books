using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models;

public class Author
{
  public long Id { get; set; }
  [Required]
  public string Name { get; set; } = null!;

  public ICollection<Book>? Books { get; set; }
}