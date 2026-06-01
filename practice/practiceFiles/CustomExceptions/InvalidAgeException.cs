namespace practice.practiceFiles.CustomExceptions;

public class InvalidAgeException : Exception
{
    public InvalidAgeException()
        : base()
    {
    }

    public InvalidAgeException(string message)
        : base(message)
    {
    }

    public InvalidAgeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
