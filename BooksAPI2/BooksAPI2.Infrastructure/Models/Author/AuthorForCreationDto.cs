using System.ComponentModel.DataAnnotations;

namespace BooksAPI2.Infrastructure.Models.Author;

public class AuthorForCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;
}