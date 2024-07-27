namespace Domain.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException() : base("موردی یافت نشد")
    {
    }

    public NotFoundException(List<string> messages) : base(messages)
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}