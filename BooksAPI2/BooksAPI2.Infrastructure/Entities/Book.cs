using System.ComponentModel.DataAnnotations;
using BooksAPI2.Infrastructure.Interfaces.Helpers;

namespace BooksAPI2.Infrastructure.Entities;

public class Book : ISoftDeletable
{
    public Guid Id { get; set; }
    [Required]
    public int Isbn { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public Guid AuthorId { get; set; }
    [Range(0,9999)] [Required]
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; } = false;
    public virtual Author Author { get; set; }
}