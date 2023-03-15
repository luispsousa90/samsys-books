using BooksAPI2.Infrastructure.Helpers;
using BooksAPI2.Infrastructure.Interfaces.Services;
using BooksAPI2.Infrastructure.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI2.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
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
    public async Task<IActionResult> GetBook(Guid id)
    {
        if (id == Guid.Empty)
        {
            return (BadRequest(id));
        }

        var book = await _bookService.GetBookById(id);

        if (book.Obj == null)
        {
            return NotFound(book.Message);
        }

        return Ok(book);
    }

    // POST: api/Books
    // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<MessagingHelper> PostBook([FromBody] BookForCreationDto createBookDto)
    {
        MessagingHelper res = new();

        if (ModelState.IsValid) return await _bookService.CreateBook(createBookDto);
        res.Success= false;
        res.SetMessage("Invalid model object");
        return res;

    }


    // PUT: api/Books/5
    // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:Guid}")]
    public async Task<MessagingHelper> PutBook(Guid id, [FromBody] BookForUpdateDto updateBookDto)
    {
        return await _bookService.UpdateBook(id, updateBookDto);
    }

    // DELETE: api/Books/5
    [HttpDelete("{id:Guid}/hard")]
    public async Task<MessagingHelper> DeleteBook(Guid id)
    {
       return await _bookService.DeleteBook(id);
    }

    // DELETE: api/Books/5
    [HttpDelete("{id:Guid}")]
    public async Task<MessagingHelper> SoftDeleteBook(Guid id)
    {
        return await _bookService.SoftDeleteBook(id);
    }
}