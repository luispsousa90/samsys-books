using AutoMapper;
using BooksAPI2.Infrastructure.Entities;
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
            /*var booksResult = _mapper.Map<PagedList<BookDto>>(books);*/
            res.Obj.Items = books.Items.Select(r => new BookDto(r)).ToList();;
            res.Obj.TotalCount = books.TotalCount;
            res.Obj.PageSize = books.PageSize;
            res.Obj.CurrentPage = books.CurrentPage;
            res.Success = true;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }

    public async Task<MessagingHelper<BookDto>> GetBookById(Guid bookId)
    {
        MessagingHelper<BookDto> res = new();

        try
        {
            var book = await _repo.BookRepository.GetBookById(bookId);
            var bookResult = _mapper.Map<BookDto>(book);
            res.Obj = bookResult;
            res.Success = true;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }
        return res;
    }

    public async Task<MessagingHelper> CreateBook(BookForCreationDto createBookDto)
    {
        MessagingHelper res = new();
        try
        {
            var book = _mapper.Map<Book>(createBookDto);
            _repo.BookRepository.CreateBook(book);
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

    public async Task<MessagingHelper> UpdateBook(Guid id, BookForUpdateDto updateBookDto)
    {
        MessagingHelper res = new();

        try
        {
            var bookEntity = await _repo.BookRepository.GetBookById(id);

            if (bookEntity == null)
            {
                res.Success = false;
                res.SetMessage("Book not found");
                return res;
            }

            _mapper.Map(updateBookDto, bookEntity);
            _repo.BookRepository.UpdateBook(bookEntity);
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

    public async Task<MessagingHelper> DeleteBook(Guid id)
    {
        MessagingHelper res = new();

        try
        {
            var book = await _repo.BookRepository.GetBookById(id);
            if (book == null)
            {
                res.Success = false;
                res.SetMessage("Book not found");
                return res;
            }
            _repo.BookRepository.DeleteBook(book);
            await _repo.SaveAsync();
            res.Success = true;
        }
        catch(Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }

    public async Task<MessagingHelper> SoftDeleteBook(Guid id)
    {
        MessagingHelper res = new();

        try
        {
            var book = await _repo.BookRepository.GetBookById(id);
            if (book == null)
            {
                res.Success = false;
                res.SetMessage("Book not found");
                return res;
            }
            _repo.BookRepository.SoftDeleteBook(book);
            await _repo.SaveAsync();
            res.Success = true;
        }
        catch(Exception ex)
        {
            res.Success = false;
            res.SetMessage(ex.Message);
        }

        return res;
    }
}