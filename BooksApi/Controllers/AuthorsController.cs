using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksApi.Models.Authors;
using BooksApi.Repository;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repo;

        public AuthorsController(IRepositoryWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authorsResult = _mapper.Map<IEnumerable<AuthorDto>>(await _repo.Author.GetAllAuthors());

            return Ok(authorsResult);
        }

        // GET: api/Authors/5
        [HttpGet("{id:long}", Name = "AuthorById")]
        public async Task<ActionResult<Author>> GetAuthor(long id)
        {
            var author = await _repo.Author.GetAuthorById(id);

            if (author == null) return NotFound();

            var authorResult = _mapper.Map<AuthorDto>(author);

            return Ok(authorResult);
        }

        // GET: api/Authors/5/books
        [HttpGet("{id:long}/books")]
        public async Task<ActionResult<Author>> GetAuthorWithBooks(long id)
        {
            var author = await _repo.Author.GetAuthorWithDetails(id);

            if (author == null) return NotFound();

            var authorResult = _mapper.Map<AuthorDto>(author);

            return Ok(authorResult);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(long id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            if (_context.Authors == null)
            {
                return Problem("Entity set 'RepositoryContext.Authors'  is null.");
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(long id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(long id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}