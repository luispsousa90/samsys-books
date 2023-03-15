using AutoMapper;
using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Helpers;
using BooksAPI2.Infrastructure.Interfaces.Repositories;
using BooksAPI2.Infrastructure.Interfaces.Services;
using BooksAPI2.Infrastructure.Models.Book;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BooksAPI2.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _repo;
    private readonly IBookService _bookService;

    public BooksController(IUnitOfWork repo, IMapper mapper, IBookService bookService)
    {
        _repo = repo;
        _mapper = mapper;
        _bookService = bookService;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<MessagingHelper<PagedList<BookDto>>> GetBooks([FromQuery] BookParameters bookParameters)
    {
        return await _bookService.GetAllBooks(bookParameters);
    }

    // GET: api/Books/5
    [HttpGet("{id:Guid}", Name = "BookById")]
    public async Task<ActionResult<Book>> GetBook(Guid id)
    {
        try
        {
            var book = await _repo.BookRepository.GetBookById(id);

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

            _repo.BookRepository.CreateBook(bookEntity);
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
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> PutBook(Guid id, [FromBody] BookForUpdateDto? book)
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

            var bookEntity = await _repo.BookRepository.GetBookById(id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(book, bookEntity);
            _repo.BookRepository.UpdateBook(bookEntity);
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
    [HttpDelete("{id:Guid}/hard")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        try
        {
            var book = await _repo.BookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _repo.BookRepository.DeleteBook(book);
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
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> SoftDeleteBook(Guid id)
    {
        try
        {
            var book = await _repo.BookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _repo.BookRepository.SoftDeleteBook(book);
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