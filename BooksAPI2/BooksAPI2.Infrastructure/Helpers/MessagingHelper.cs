using System.Text.Json.Serialization;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;


namespace BooksAPI2.Infrastructure.Helpers;

public class MessagingHelper
{
    public bool Success { get; set; }
    private string Message { get; set; }

    [JsonConverter(typeof(SmartEnumValueConverter<ErrorType, string>))]
    public ErrorType? ErrorType { get; set; } = null;

    public void SetMessage(string message)
    {
        Message = message;
    }
}

public class MessagingHelper<T> : MessagingHelper
{
    public T? Obj { get; set; }
}

public class ErrorType : SmartEnum<ErrorType, string>
{
    public static readonly ErrorType DataHasChanged = new ErrorType("DataHasChanged");
    private ErrorType(string name) : base(name, name) { }
}