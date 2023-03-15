using System.Collections;
using BooksAPI2.Infrastructure.Helpers;
using BooksAPI2.Infrastructure.Models.Author;

namespace BooksAPI2.Infrastructure.Interfaces.Services;

public interface IAuthorService
{
    Task<MessagingHelper<IEnumerable<AuthorDto>>> GetAllAuthors();
    Task<MessagingHelper<AuthorDto>> GetAuthorById(Guid authorId);
    Task<MessagingHelper<AuthorDto>> GetAuthorWithDetails(Guid authorId);
    Task<MessagingHelper> CreateAuthor(AuthorForCreationDto createAuthorDto);
    Task<MessagingHelper> UpdateAuthor(Guid id, AuthorForUpdateDto updateAuthorDto);
    Task<MessagingHelper> DeleteAuthor(Guid id);

    Task<MessagingHelper> SoftDeleteAuthor(Guid id);
}