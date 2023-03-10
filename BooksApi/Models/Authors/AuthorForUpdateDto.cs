using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models.Authors;

public class AuthorForUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;
}