namespace MilGlorian.Persistence.Exceptions;

public class NullorEmptyException : Exception
{
    public NullorEmptyException(string? message) : base(message)
    {
    }
}
