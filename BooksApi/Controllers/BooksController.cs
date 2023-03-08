using AutoMapper;
using BooksApi.Models.Books;
using BooksApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repo;

    public BooksController(IRepositoryWrapper repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var booksResult = _mapper.Map<IEnumerable<BookDto>>(await _repo.Book.GetAllBooks().ToListAsync());

        return Ok(booksResult);
    }

    // GET: api/Books/5
    [HttpGet("{id:long}")]
    public async Task<ActionResult<Book>> GetBook(long id)
    {
        var book = await _repo.Book.GetBookById(id).FirstOrDefaultAsync();

        if (book == null) return NotFound();

        var bookResult = _mapper.Map<BookDto>(book);

        return Ok(bookResult);
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /*[HttpPut("{id}")]
    public async Task<IActionResult> PutBook(long id, Book book)
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
    }*/

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /*[HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
      if (_context.Books == null)
      {
          return Problem("Entity set 'RepositoryContext.Books'  is null.");
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
    }*/
}