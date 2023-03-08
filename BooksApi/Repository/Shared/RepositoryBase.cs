using System.Linq.Expressions;
using BooksApi.Data;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repository.Shared
{
  public abstract class Repository<T> : IRepositoryBase<T> where T : class
  {
    private RepositoryContext RepositoryContext { get; set; }

    protected Repository(RepositoryContext repositoryContext)
    {
      RepositoryContext = repositoryContext;
    }
    public IQueryable<T> FindAll()
    {
      return RepositoryContext.Set<T>().AsNoTracking();
    }
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
      return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
    }
    public void Create(T entity)
    {
      RepositoryContext.Set<T>().Add(entity);
    }
    public void Update(T entity)
    {
      RepositoryContext.Set<T>().Update(entity);
    }
    public void Delete(T entity)
    {
      RepositoryContext.Set<T>().Remove(entity);
    }
  }
}