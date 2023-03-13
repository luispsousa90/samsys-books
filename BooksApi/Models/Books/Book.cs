using System.ComponentModel.DataAnnotations;
using BooksApi.Models.Authors;
using BooksApi.Repository.Shared;


namespace BooksApi.Models.Books;

public class Book : ISoftDeletable
{
    public long Id { get; set; }
    [Required] public int Isbn { get; set; }
    [Required] public string Name { get; set; } = null!;
    [Required] public long AuthorId { get; set; }
    [Range(0, 99999)] [Required] public decimal Price { get; set; }
    public bool IsDeleted { get; set; } = false;

    public virtual Author Author { get; set; }
}