﻿using AutoMapper;
using BooksApi.Models.Authors;
using BooksApi.Models.Books;

namespace BooksApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorForCreationDto, Author>();
        CreateMap<AuthorForUpdateDto, Author>();
        CreateMap<Book, BookDto>();
        CreateMap<BookForCreationDto, Book>();
        CreateMap<BookForUpdateDto, Book>();
    }
}