using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Models.Books;
using BooksApi.Repository.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Repository.Books
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IOrderedQueryable<Book> GetAllBooks()
        {
            return FindAll().OrderBy(book => book.Name);
        }

        public IQueryable<Book> GetBookById(long bookId)
        {
            return FindByCondition(book => book.Id == bookId);
        }

        public IQueryable<Book> CreateBook(Book book) => throw new NotImplementedException();

        public IQueryable<Book> UpdateBook(long id, Book book)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteBook(long id)
        {
            throw new NotImplementedException();
        }
    }
}