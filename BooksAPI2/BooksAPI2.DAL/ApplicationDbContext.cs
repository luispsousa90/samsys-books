using BooksAPI2.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI2.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>()
            .HasIndex(b => b.Isbn)
            .IsUnique();
        builder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
    }
}