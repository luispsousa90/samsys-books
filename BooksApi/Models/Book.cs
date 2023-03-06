namespace BooksApi.Models;

public class Book
{
  public long Id { get; set; }
  public int Isbn { get; set; }
  public string Name { get; set; } = null!;
  public string Author { get; set; } = null!;
  public decimal Price { get; set; }
}