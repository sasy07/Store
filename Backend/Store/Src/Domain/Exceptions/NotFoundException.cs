namespace Domain.Exceptions;

public class NotFoundEntityException : BaseException
{
    public NotFoundEntityException() : base("موردی یافت نشد")
    {
    }

    public NotFoundEntityException(List<string> messages) : base(messages)
    {
    }

    public NotFoundEntityException(string message) : base(message)
    {
    }
}