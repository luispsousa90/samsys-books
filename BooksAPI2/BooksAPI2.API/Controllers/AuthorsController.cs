using AutoMapper;
using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Helpers;
using BooksAPI2.Infrastructure.Interfaces.Repositories;
using BooksAPI2.Infrastructure.Interfaces.Services;
using BooksAPI2.Infrastructure.Models.Author;
using Microsoft.AspNetCore.Mvc;


namespace BooksAPI2.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<MessagingHelper<IEnumerable<AuthorDto>>> GetAuthors()
        {
            return await _authorService.GetAllAuthors();
        }

        // GET: api/Authors/5
        [HttpGet("{id:Guid}", Name = "AuthorById")]
        public async Task<MessagingHelper<AuthorDto>> GetAuthor(Guid id)
        {
            return await _authorService.GetAuthorById(id);
        }

        // GET: api/Authors/5/books
        [HttpGet("{id:Guid}/books")]
        public async Task<MessagingHelper<AuthorDto>> GetAuthorWithBooks(Guid id)
        {
            return await _authorService.GetAuthorWithDetails(id);
        }

        // POST: api/Authors
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<MessagingHelper> PostAuthor([FromBody] AuthorForCreationDto createAuthorDto)
        {
            MessagingHelper res = new();
            if (ModelState.IsValid) return await _authorService.CreateAuthor(createAuthorDto);
            res.Success = false;
            res.SetMessage("Invalid model object");
            return res;
        }

        // PUT: api/Authors/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<MessagingHelper> PutAuthor(Guid id, [FromBody] AuthorForUpdateDto authorUpdateDto)
        {
            return await _authorService.UpdateAuthor(id, authorUpdateDto);
        }
    }