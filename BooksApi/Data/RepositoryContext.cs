using BooksApi.Models;
using BooksApi.Models.Authors;
using BooksApi.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Data;

public class RepositoryContext : DbContext
{
  public RepositoryContext(DbContextOptions<RepositoryContext> options)
      : base(options)
  {
  }

  public DbSet<Book> Books { get; set; } = null!;
  public DbSet<Author> Authors { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.Entity<Book>()
        .HasIndex(u => u.Isbn)
        .IsUnique();
  }
}