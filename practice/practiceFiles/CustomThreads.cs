namespace practice.practiceFiles;

public class CustomThreads
{
    // creating thread
    public static void ThreadMethod()
    {
        Thread thread = new Thread(DoWork);
        thread.Start();

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Main thread: " + i);
            Thread.Sleep(500);
        }

        /*
            1.ThreadMethod() starts executing in the main thread:

            Creates a new Thread object that will run the DoWork method
            Calls thread.Start() which launches the worker thread

            2.After this, two threads run concurrently:

            The main thread continues executing the for loop in ThreadMethod()
            The worker thread executes the for loop in DoWork()

            3.The timing differences:

            Main thread: Sleeps for 500ms between iterations
            Worker thread: Sleeps for 700ms between iterations
         */
    }

    static void DoWork()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Worker thread: " + i);
            Thread.Sleep(700);
        }
    }

    // creating parameterized thread
    public static void ParameterizedThreadMethod()
    {
        Thread thread = new Thread(() => printMassage("Hello", 4));
        thread.Start();
    }

    static void printMassage(string msg, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(msg);
        }
    }

    // Thread synchronization using lock

    private static object _locker = new object(); // Use as a synchronization object to ensure only one thread can enter the critical section at a time.
    private static int _counter = 0;
    static void Increment()
    {
        lock (_locker) // locks on _locker before incrementing _counter
        {
            _counter++;
            Console.WriteLine($"Counter incremented to {_counter} by thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public static void ThreadSynchronization()
    {
        Thread[] threads = new Thread[10];

        for (int i = 0; i < threads.Length; i++)
        {
            // Create and start 10 threads that call Increment
            threads[i] = new Thread(Increment);
            threads[i].Start();
        }

        // Wait for all threads to finish
        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine($"Final counter value: {_counter}");

        /*
            Multiple threads call Increment concurrently, 
            but the lock ensures increments happen safely without race conditions.
            The final output shows the counter incremented exactly 10 times.
         */
    }

    // Thread safety using Mutex

    public static void ThreadSafetyUsingMutex()
    {
        using Mutex mutex = new Mutex(false, "Sample_Mutex");

        Console.WriteLine("Waiting to enter critical section...");
        mutex.WaitOne();  // Wait until it is safe to enter the critical section

        try
        {
            Console.WriteLine("Entered critical section.");
            Console.WriteLine("Press Enter to exit and release the mutex.");

            Console.ReadLine();
        }
        finally
        {
            mutex.ReleaseMutex();
            Console.WriteLine("Mutex released.");
        }

        /*
            The critical section is between WaitOne() and ReleaseMutex().
            This pattern is commonly used to prevent multiple instances 
            of an application from running simultaneously.
        */
    }

    // Thread safety using Semaphore

    private static Semaphore _semaphore = new Semaphore(3, 3);

    static void AccessResource(object id)
    {
        Console.WriteLine($"Thread {id} is waiting to enter the semaphore.");

        _semaphore.WaitOne();  // Wait to enter the semaphore

        try
        {
            Console.WriteLine($"Thread {id} has entered the semaphore.");

            Thread.Sleep(2000);
            Console.WriteLine($"Thread {id} is leaving the semaphore.");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public static void ThreadSafetyUsingSemaphore()
    {
        // Create and start 10 threads
        for (int i = 1; i <= 10; i++)
        {
            int threadId = i; // To avoid modified closure issue
            new Thread(() => AccessResource(threadId)).Start();
        }
    }

    // Thread pool

    public static void ThreadPoolExecution()
    {
        Console.WriteLine($"Main thread started. Thread ID: {Environment.CurrentManagedThreadId}");

        // Track completion of work items
        CountdownEvent completion = new CountdownEvent(3);

        // Worker method that processes data
        void ProcessWorkItem(object? state)
        {
            var workId = state as string ?? "Unknown";
            Console.WriteLine($"Processing work item {workId} on thread {Environment.CurrentManagedThreadId}");

            Thread.Sleep(1000);

            Console.WriteLine($"Completed work item {workId} on thread {Environment.CurrentManagedThreadId}");
            completion.Signal();
        }

        // Queue multiple work items
        for (int i = 1; i <= 3; i++)
        {
            ThreadPool.QueueUserWorkItem(ProcessWorkItem, $"Item {i}");
            Console.WriteLine($"Queued work item {i} from thread {Environment.CurrentManagedThreadId}");
        }

        // Wait for all work items to complete
        completion.Wait();

        Console.WriteLine("All work items completed.");
        Console.WriteLine("Main thread ending.");
    }

    // Async and Await with Tasks
}
