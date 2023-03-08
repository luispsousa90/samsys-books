using AutoMapper;
using BooksApi.Models.Authors;
using BooksApi.Models.Books;

namespace BooksApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<Book, BookDto>();
    }   
}