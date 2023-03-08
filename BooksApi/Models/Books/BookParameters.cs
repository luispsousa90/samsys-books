using BooksApi.Helpers;

namespace BooksApi.Models.Books;

public class BookParameters : QueryStringParameters
{
  public BookParameters()
  {
    OrderBy = "name";
  }
}