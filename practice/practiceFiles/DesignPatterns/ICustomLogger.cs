namespace practice.practiceFiles.DesignPatterns;

// Product interface
public interface ICustomLogger
{
    void Log(string message);
}

// Concrete products
public class FileLogger : ICustomLogger
{
    public void Log(string message) => Console.WriteLine($"File: {message}");
}

public class DatabaseLogger : ICustomLogger
{
    public void Log(string message) => Console.WriteLine($"Database: {message}");
}
