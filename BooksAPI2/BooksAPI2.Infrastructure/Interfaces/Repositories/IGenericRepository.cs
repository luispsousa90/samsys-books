using System.Linq.Expressions;

namespace BooksAPI2.Infrastructure.Interfaces.Repositories;

public interface IGenericRepository<T>
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    void SoftDelete(T entity);
}