using BooksApi.Models;

public class BookParameters : QueryStringParameters
{
  public BookParameters()
  {
    OrderBy = "name";
  }
}

