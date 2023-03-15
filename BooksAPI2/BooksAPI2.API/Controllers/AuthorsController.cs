using AutoMapper;
using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Interfaces.Repositories;
using BooksAPI2.Infrastructure.Models.Author;
using Microsoft.AspNetCore.Mvc;


namespace BooksAPI2.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _repo;

        public AuthorsController(IUnitOfWork repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authorsResult = _mapper.Map<IEnumerable<AuthorDto>>(await _repo.AuthorRepository.GetAllAuthors());

            return Ok(authorsResult);
        }

        // GET: api/Authors/5
        [HttpGet("{id:Guid}", Name = "AuthorById")]
        public async Task<ActionResult<Author>> GetAuthor(Guid id)
        {
            var author = await _repo.AuthorRepository.GetAuthorById(id);

            if (author == null) return NotFound();

            var authorResult = _mapper.Map<AuthorDto>(author);

            return Ok(authorResult);
        }

        // GET: api/Authors/5/books
        [HttpGet("{id:Guid}/books")]
        public async Task<ActionResult<Author>> GetAuthorWithBooks(Guid id)
        {
            var author = await _repo.AuthorRepository.GetAuthorWithDetails(id);

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

                _repo.AuthorRepository.CreateAuthor(authorEntity);
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
        public async Task<IActionResult> PutAuthor(Guid id, [FromBody] AuthorForUpdateDto? author)
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

                var authorEntity = await _repo.AuthorRepository.GetAuthorById(id);
                if (authorEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(author, authorEntity);
                _repo.AuthorRepository.UpdateAuthor(authorEntity);
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