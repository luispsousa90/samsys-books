using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksApi.Models;

namespace BooksApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly BookContext _context;

    public BooksController(BookContext context)
    {
      _context = context;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks([FromQuery] BookParameters bookParameters)
    {
      if (_context.Books == null)
      {
        return NotFound();
      }

      return await _context.Books.OrderBy(book => book.Name)
        .Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)
        .Take(bookParameters.PageSize)
        .ToListAsync();

      //return await _context.Books.ToListAsync();
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(long id)
    {
      if (_context.Books == null)
      {
        return NotFound();
      }
      var book = await _context.Books.FindAsync(id);

      if (book == null)
      {
        return NotFound();
      }

      return book;
    }

    // GET: api/Books/isbn/5
    [HttpGet("isbn/{isbn}")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBookByIsbn(int isbn)
    {
      if (_context.Books == null)
      {
        return NotFound();
      }
      var books = _context.Books.Where(x => x.Isbn == isbn);

      if (books == null)
      {
        return NotFound();
      }

      return await books.ToListAsync();
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(long id, Book book)
    {
      if (id != book.Id)
      {
        return BadRequest();
      }

      _context.Entry(book).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BookExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(Book book)
    {
      if (_context.Books == null)
      {
        return Problem("Entity set 'BookContext.Books'  is null.");
      }
      _context.Books.Add(book);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetBook", new { id = book.Id }, book);
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(long id)
    {
      if (_context.Books == null)
      {
        return NotFound();
      }
      var book = await _context.Books.FindAsync(id);
      if (book == null)
      {
        return NotFound();
      }

      _context.Books.Remove(book);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool BookExists(long id)
    {
      return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
