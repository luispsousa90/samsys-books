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
        try
        {
            var books = await _repo.Book.GetAllBooks();
            var booksResult = _mapper.Map<IEnumerable<BookDto>>(books);

            return Ok(booksResult);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/Books/5
    [HttpGet("{id:long}", Name = "BookById")]
    public async Task<ActionResult<Book>> GetBook(long id)
    {
        try
        {
            var book = await _repo.Book.GetBookById(id).FirstOrDefaultAsync();

            if (book == null) return NotFound();

            var bookResult = _mapper.Map<BookDto>(book);

            return Ok(bookResult);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book? book)
    {
        try
        {
            if (book == null)
            {
                BadRequest("Book object is null");
            }

            var bookEntity = _mapper.Map<Book>(book);

            _repo.Book.CreateBook(bookEntity);
            await _repo.SaveAsync();

            var createdBook = _mapper.Map<BookDto>(bookEntity);

            return CreatedAtRoute("BookById", new { id = createdBook.Id }, createdBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
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

    /*
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