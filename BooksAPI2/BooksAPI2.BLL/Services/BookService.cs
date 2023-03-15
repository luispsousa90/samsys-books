using AutoMapper;
using BooksAPI2.Infrastructure.Helpers;
using BooksAPI2.Infrastructure.Interfaces.Repositories;
using BooksAPI2.Infrastructure.Interfaces.Services;
using BooksAPI2.Infrastructure.Models.Book;

namespace BooksAPI2.BLL.Services;

public class BookService: IBookService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _repo;

    public BookService(IUnitOfWork repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<MessagingHelper<PagedList<BookDto>>> GetAllBooks(BookParameters bookParameters)
    {
        var res = new MessagingHelper<PagedList<BookDto>>() { Obj = new PagedList<BookDto>()};
        try
        {
            var books = await _repo.BookRepository.GetAllBooks(bookParameters);
            var booksResult = _mapper.Map<PagedList<BookDto>>(books);
            res.Obj = booksResult;
            res.Success = true;
            return res;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }

    public Task<MessagingHelper<BookDto?>> GetBookById(Guid bookId)
    {
        throw new NotImplementedException();
    }

    public Task<MessagingHelper> CreateBook(BookForCreationDto createBookDto)
    {
        throw new NotImplementedException();
    }

    public Task<MessagingHelper> UpdateBook(BookForUpdateDto book)
    {
        throw new NotImplementedException();
    }

    public Task<MessagingHelper> DeleteBook(BookDto book)
    {
        throw new NotImplementedException();
    }

    public Task<MessagingHelper> SoftDeleteBook(BookDto book)
    {
        throw new NotImplementedException();
    }
}