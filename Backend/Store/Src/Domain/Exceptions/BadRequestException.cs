namespace Domain.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(List<string> messages) : base(messages)
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException() : base("خطایی رخ داده است لطفا دوباره تلاش کنید")
    {
        
    }
}