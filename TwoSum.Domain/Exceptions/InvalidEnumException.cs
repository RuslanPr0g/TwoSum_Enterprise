namespace TwoSum.Domain.Exceptions;

public sealed class InvalidEnumException : Exception
{
    public InvalidEnumException() : base()
    {
    }

    public InvalidEnumException(string message) : base(message)
    {
    }
}
