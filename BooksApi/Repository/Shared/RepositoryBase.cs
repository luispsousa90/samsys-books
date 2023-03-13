using System.Linq.Expressions;
using BooksApi.Data;
using BooksApi.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repository.Shared
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private RepositoryContext RepositoryContext { get; set; }

        protected RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<Book> FindAllWithAuthors()
        {
            return RepositoryContext.Books
                .Include(book => book.Author)
                .AsNoTracking();
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

        public void SoftDelete(T entity)
        {
            if (entity is ISoftDeletable softDeletableEntity)
            {
                // If the entity implements the ISoftDeletable interface, update its IsDeleted property
                softDeletableEntity.IsDeleted = true;
                RepositoryContext.Set<T>().Update(entity);
            }
            else
            {
                // Otherwise, throw an exception
                throw new InvalidOperationException("The entity does not implement the ISoftDeletable interface.");
            }
        }
    }
}