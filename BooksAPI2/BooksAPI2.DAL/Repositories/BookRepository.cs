﻿using System.Reflection;
using System.Text;
using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Helpers;
using BooksAPI2.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BooksAPI2.DAL.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<PagedList<Book>> GetAllBooks(BookParameters bookParameters)
        {
            IQueryable<Book> books = FindAllWithAuthorName();

            SearchByIsbn(ref books, bookParameters.Isbn);
            SearchByAuthor(ref books, bookParameters.AuthorId);
            if (bookParameters.Name != null) SearchByName(ref books, bookParameters.Name);

            if (bookParameters.OrderBy != null) ApplySort(ref books, bookParameters.OrderBy);

            return await PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public async Task<Book?> GetBookById(Guid bookId)
        {
            return await FindByCondition(book => book.Id.Equals(bookId)).FirstOrDefaultAsync();
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

        public void SoftDeleteBook(Book book)
        {
            SoftDelete(book);
        }

        private static void ApplySort(ref IQueryable<Book> books, string orderByQueryString)
        {
            if (!books.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                books = books.OrderBy(x => x.Name);
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Book).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                books = books.OrderBy(x => x.Name);
                return;
            }

            books = books.OrderBy(orderQuery);
        }

        private static void SearchByIsbn(ref IQueryable<Book> books, int isbn)
        {
            if (!books.Any() || isbn == 0)
                return;
            books = books.Where(o => o.Isbn == isbn);
        }

        private static void SearchByName(ref IQueryable<Book> books, string bookName)
        {
            if (!books.Any() || string.IsNullOrWhiteSpace(bookName))
                return;
            books = books.Where(o => o.Name.ToLower().Contains(bookName.Trim().ToLower()));
        }

        private static void SearchByAuthor(ref IQueryable<Book> books, Guid authorId)
        {
            if (!books.Any() || authorId == Guid.Empty)
                return;
            books = books.Where(o => o.AuthorId.Equals(authorId));
        }
    }
