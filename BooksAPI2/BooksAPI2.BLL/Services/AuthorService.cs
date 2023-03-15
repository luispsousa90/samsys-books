using AutoMapper;
using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Helpers;
using BooksAPI2.Infrastructure.Interfaces.Repositories;
using BooksAPI2.Infrastructure.Interfaces.Services;
using BooksAPI2.Infrastructure.Models.Author;

namespace BooksAPI2.BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _repo;

    public AuthorService(IUnitOfWork repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<MessagingHelper<IEnumerable<AuthorDto>>> GetAllAuthors()
    {
        MessagingHelper<IEnumerable<AuthorDto>> res = new();
        try
        {
            var authors = await _repo.AuthorRepository.GetAllAuthors();
            var authorsResult = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            res.Obj = authorsResult;
            res.Success = true;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }

    public async Task<MessagingHelper<AuthorDto>> GetAuthorById(Guid authorId)
    {
        MessagingHelper<AuthorDto> res = new();

        try
        {
            var author = await _repo.AuthorRepository.GetAuthorById(authorId);
            var authorResult = _mapper.Map<AuthorDto>(author);
            res.Obj = authorResult;
            res.Success = true;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }

    public async Task<MessagingHelper<AuthorDto>> GetAuthorWithDetails(Guid authorId)
    {
        var res = new MessagingHelper<AuthorDto>() { Obj = new AuthorDto() };

        try
        {
            var author = await _repo.AuthorRepository.GetAuthorWithDetails(authorId);
            var authorResult = _mapper.Map<AuthorDto>(author);
            res.Obj = authorResult;
            res.Success = true;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }

    public async Task<MessagingHelper> CreateAuthor(AuthorForCreationDto createAuthorDto)
    {
        MessagingHelper res = new();
        try
        {
            var author = _mapper.Map<Author>(createAuthorDto);
            _repo.AuthorRepository.CreateAuthor(author);
            await _repo.SaveAsync();
            res.Success = true;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }

    public async Task<MessagingHelper> UpdateAuthor(Guid id, AuthorForUpdateDto updateAuthorDto)
    {
        MessagingHelper res = new();
        try
        {
            var authorEntity = await _repo.AuthorRepository.GetAuthorById(id);
            if (authorEntity == null)
            {
                res.Success = false;
                res.SetMessage("Author not found");
                return res;
            }

            _mapper.Map(updateAuthorDto, authorEntity);
            _repo.AuthorRepository.UpdateAuthor(authorEntity);
            await _repo.SaveAsync();
            res.Success = true;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }

    public Task<MessagingHelper> DeleteAuthor(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<MessagingHelper> SoftDeleteAuthor(Guid id)
    {
        throw new NotImplementedException();
    }
}