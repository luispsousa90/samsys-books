using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        // POST: api/Authors
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor([FromBody] AuthorForCreationDto? author)
        {
            try
            {
                if (author == null)
                {
                    return BadRequest();
                }

                var authorEntity = _mapper.Map<Author>(author);

                _repo.Author.CreateAuthor(authorEntity);
                await _repo.SaveAsync();

                var createdAuthor = _mapper.Map<AuthorDto>(authorEntity);

                return CreatedAtAction("GetAuthor", new { id = createdAuthor.Id }, createdAuthor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Authors/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(long id, [FromBody] AuthorForUpdateDto? author)
        {
            try
            {
                if (author == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var authorEntity = await _repo.Author.GetAuthorById(id);
                if (authorEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(author, authorEntity);
                _repo.Author.UpdateAuthor(authorEntity);
                await _repo.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }


        // DELETE: api/Authors/5
        /*[HttpDelete("{id}")]
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