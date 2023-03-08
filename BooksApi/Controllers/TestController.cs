using BooksApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private IRepositoryWrapper _repoWrapper;

    public TestController(IRepositoryWrapper repoWrapper)
    {
        _repoWrapper = repoWrapper;
    }

    /*[HttpGet]
    public IEnumerable<string> Get()
    {
        var books = _repoWrapper.Book.FindAll();
        var authors = _repoWrapper.Author.FindAll();

        return [];
    }*/
}