using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models;

public class Book
{
  public long Id { get; set; }
  [Required]
  public int Isbn { get; set; }
  [Required]
  public string Name { get; set; } = null!;
  [Required]
  public long AuthorId { get; set; }
  [Range(0, 999999)]
  public decimal Price { get; set; }
}