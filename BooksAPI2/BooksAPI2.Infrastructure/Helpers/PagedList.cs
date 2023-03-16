using Microsoft.EntityFrameworkCore;
namespace BooksAPI2.Infrastructure.Helpers;
public class PagedList<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public IEnumerable<T> Items { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(){}

    private PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
        /*AddRange(items);*/
    }

    public static async Task<PagedList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}