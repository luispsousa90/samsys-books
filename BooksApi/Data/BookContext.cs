using Microsoft.EntityFrameworkCore;

namespace BooksApi.Models;

public class BookContext : DbContext
{
  public BookContext(DbContextOptions<BookContext> options)
      : base(options)
  {
  }

  public DbSet<Book> Books { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.Entity<Book>()
        .HasIndex(u => u.Isbn)
        .IsUnique();
  }
}