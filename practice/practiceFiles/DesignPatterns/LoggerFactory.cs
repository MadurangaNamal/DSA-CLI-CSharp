namespace practice.practiceFiles.DesignPatterns;

public abstract class LoggerFactory
{
    public abstract ICustomLogger CreateLogger();
}

// Concrete creators
public class FileLoggerFactory : LoggerFactory
{
    public override ICustomLogger CreateLogger() => new FileLogger();
}

public class DatabaseLoggerFactory : LoggerFactory
{
    public override ICustomLogger CreateLogger() => new DatabaseLogger();
}
