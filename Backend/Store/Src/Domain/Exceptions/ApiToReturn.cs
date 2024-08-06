namespace Domain.Exceptions;

public class ApiToReturn
{
    public ApiToReturn()
    {
    }

    public ApiToReturn(string message)
    {
        Message = message;
        Messages.Add(message);
    }

    public ApiToReturn(int statusCode, string message)
    {
        Message = message;
        StatusCode = statusCode;
        Messages.Add(message);
    }


    public ApiToReturn(int statusCode, List<string> messages)
    {
        StatusCode = statusCode;
        Messages = messages;
    }

    public ApiToReturn(int statusCode, List<string> messages, string detail)
    {
        StatusCode = statusCode;
        Messages = messages;
        Detail = detail;
    }

    public ApiToReturn(int statusCode, string message, string detail)
    {
        StatusCode = statusCode;
        Message = message;
        Messages.Add(message);
        Detail = detail;
    }

    public ApiToReturn(int statusCode, List<string> messages, string message, string detail)
    {
        StatusCode = statusCode;
        Message = message;
        Messages = messages;
        Detail = detail;
    }

    public string Message { get; set; }
    public int StatusCode { get; set; }
    public string Detail { get; set; }
    public List<string> Messages { get; set; } = new();
}