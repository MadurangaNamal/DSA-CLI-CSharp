namespace practice.practiceFiles.DesignPatterns;

public sealed class SingletonPattern
{
    private static volatile SingletonPattern _instance = null!;
    private static readonly object _lock = new object();
    private readonly int _counter = 0;

    private SingletonPattern()
    {
        Interlocked.Increment(ref _counter); // Thread-safe increment   
        Console.WriteLine($"Singleton instance created. Count: {_counter}");
    }

    public static SingletonPattern Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock) // Lock to ensure only one thread creates the instance
                {
                    if (_instance == null) // Re-check inside lock in case another thread created the instance while waiting
                    {
                        _instance = new SingletonPattern();
                    }
                }
            }
            return _instance;
        }
    }

}
