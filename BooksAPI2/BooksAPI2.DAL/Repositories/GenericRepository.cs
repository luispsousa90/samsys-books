using System.Linq.Expressions;
using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Interfaces.Helpers;
using BooksAPI2.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI2.DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private ApplicationDbContext ApplicationDbContext { get; set; }

    protected GenericRepository(ApplicationDbContext repositoryContext)
    {
        ApplicationDbContext = repositoryContext;
    }

    public IQueryable<T> FindAll()
    {
        return ApplicationDbContext.Set<T>().AsNoTracking();
    }

    public IQueryable<Book> FindAllWithAuthors()
    {
        return ApplicationDbContext.Books
            .Include(book => book.Author)
            .AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return ApplicationDbContext.Set<T>().Where(expression).AsNoTracking();
    }

    public void Create(T entity)
    {
        ApplicationDbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        ApplicationDbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        ApplicationDbContext.Set<T>().Remove(entity);
    }

    public void SoftDelete(T entity)
    {
        if (entity is ISoftDeletable softDeletableEntity)
        {
            // If the entity implements the ISoftDeletable interface, update its IsDeleted property
            softDeletableEntity.IsDeleted = true;
            ApplicationDbContext.Set<T>().Update(entity);
        }
        else
        {
            // Otherwise, throw an exception
            throw new InvalidOperationException("The entity does not implement the ISoftDeletable interface.");
        }
    }
}