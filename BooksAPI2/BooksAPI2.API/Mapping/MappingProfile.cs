using AutoMapper;
using BooksAPI2.Infrastructure.Entities;
using BooksAPI2.Infrastructure.Models.Author;
using BooksAPI2.Infrastructure.Models.Book;

namespace BooksAPI2.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorForCreationDto, Author>();
        CreateMap<AuthorForUpdateDto, Author>();
        CreateMap<Book, BookDto>().ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
        CreateMap<BookForCreationDto, Book>();
        CreateMap<BookForUpdateDto, Book>();
    }
}