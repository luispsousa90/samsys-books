using AutoMapper;
using BooksApi.Models.Books;
using BooksApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] BookParameters bookParameters)
    {
        try
        {
            var books = await _repo.Book.GetAllBooks(bookParameters);
            var booksResult = _mapper.Map<IEnumerable<BookDto>>(books);

            var metadata = new
            {
                books.TotalCount,
                books.PageSize,
                books.CurrentPage,
                books.TotalPages,
                books.HasNext,
                books.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(booksResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/Books/5
    [HttpGet("{id:long}", Name = "BookById")]
    public async Task<ActionResult<Book>> GetBook(long id)
    {
        try
        {
            var book = await _repo.Book.GetBookById(id);

            if (book == null) return NotFound();

            var bookResult = _mapper.Map<BookDto>(book);

            return Ok(bookResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/Books
    // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook([FromBody] BookForCreationDto? book)
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
            Console.WriteLine("Error: " + ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }


    // PUT: api/Books/5
    // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutBook(long id, [FromBody] BookForUpdateDto? book)
    {
        try
        {
            if (book == null)
            {
                return BadRequest("Owner object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var bookEntity = await _repo.Book.GetBookById(id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(book, bookEntity);
            _repo.Book.UpdateBook(bookEntity);
            await _repo.SaveAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/Books/5
    [HttpDelete("{id:long}/hard")]
    public async Task<IActionResult> DeleteBook(long id)
    {
        try
        {
            var book = await _repo.Book.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _repo.Book.DeleteBook(book);
            await _repo.SaveAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/Books/5
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> SoftDeleteBook(long id)
    {
        try
        {
            var book = await _repo.Book.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _repo.Book.SoftDeleteBook(book);
            await _repo.SaveAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }
}