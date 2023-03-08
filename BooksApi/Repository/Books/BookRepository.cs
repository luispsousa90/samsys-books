using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Models.Books;
using BooksApi.Repository.Shared;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repository.Books
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await FindAll().OrderBy(book => book.Name).ToListAsync();
        }

        public async Task<Book> GetBookById(long bookId)
        {
            return await FindByCondition(book => book.Id == bookId).FirstOrDefaultAsync();
        }

        public void CreateBook(Book book)
        {
            Create(book);
        }

        public void UpdateBook(Book book)
        {
            Update(book);
        }

        public void DeleteBook(Book book)
        {
            Delete(book);
        }
    }
}