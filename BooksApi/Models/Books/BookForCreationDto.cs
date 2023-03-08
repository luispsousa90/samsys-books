using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models.Books;

public class BookForCreationDto
{
    [Required(ErrorMessage = "ISBN is required")]
    public int Isbn { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "AuthorId is required")]
    public long AuthorId { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, 99999)]
    public decimal Price { get; set; }
}