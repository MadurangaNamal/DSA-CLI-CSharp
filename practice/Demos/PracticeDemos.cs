using AutoMapper;
using Microsoft.Extensions.Logging;
using Practice.DesignPatterns;
using Practice.Exceptions;
using Practice.Mapping;
using Practice.Models;
using Practice.Models.Samples;
using System.Linq.Dynamic.Core;
using System.Runtime.InteropServices;

namespace Practice.Demos;

public static class PracticeDemos
{
    public static void PrintValues()
    {
        string value = "abcd";
        ChangeValue(value);

        Console.WriteLine(value);
    }

    private static void ChangeValue(string value)
    {
        value = null!;
    }

    public static void SquareRef(ref int n)
    {
        n = n * n;
        Console.WriteLine($"Value inside the SquareRef function: {n}");
    }

    public static void Square(int n)
    {
        n = n * n;
        Console.WriteLine($"Value inside the Square function: {n}");
    }

    public static void PrintStruct(SampleStruct theStruct)
    {
        theStruct.a = 20;
        theStruct.b = false;
        theStruct.value = "Modified local copy";

        Console.WriteLine($"Struct values {theStruct.a} , {theStruct.b}, {theStruct.value}");
    }

    public static void PrintClass(SampleClass theClass)
    {
        theClass.a = 20;
        theClass.b = false;
        theClass.value = "Changed";

        Console.WriteLine($"Class values {theClass.a} , {theClass.b}, {theClass.value}");
    }

    public static void PrintRecord(SampleRecord theRecord)
    {
        //theRecord.a = 20; // Error: Records are immutable
        //theRecord.b = false; // Error
        //theRecord.value = null; // Error

        Console.WriteLine($"Record values {theRecord.a} , {theRecord.b}, {theRecord.value}");
    }

    public static void PrintRecordIllustrations()
    {
        ProductDto miloDrink = new(Guid.NewGuid(), "Milo Drink", "Milo drink 250ml can", 9.99m, true);
        ProductDto pepsiDrink = new(Guid.NewGuid(), "Pepsi", "Pepsi classic 200ml can", 2.99m, true);
        ProductDto pepsiDrinkDuplicate = new(pepsiDrink.ProductId, "Pepsi", "Pepsi classic 200ml can", 2.99m, true);

        Console.WriteLine(pepsiDrink == pepsiDrinkDuplicate); // value equality
        Console.WriteLine(ReferenceEquals(pepsiDrink, pepsiDrinkDuplicate));

        var product3 = miloDrink with { IsAvailable = false };

        Console.WriteLine(product3); // automatic ToString()

        var (id, name, desc, price, available) = pepsiDrink; // deconstruction

        Console.WriteLine($"{id}, {name}, {desc}, {price}, {available}");

        ///////////////////////

        Shape s1 = new Circle(10);
        Shape s2 = new Circle(10);
        Console.WriteLine(s1.Equals(s2)); // value equality

        var s3 = new Rectangle(10, 6);
        // s3.Wdith = 5; // compile error (cannot assign init-only property - Not allowed)
    }

    // Automapper
    public static void PrintAutomapperFunctions()
    {
        var config = new MapperConfiguration(
            cfg => cfg.AddProfile<PersonProfile>(),
            (ILoggerFactory?)null);
        var mapper = config.CreateMapper();

        PersonDto dataDto = new(
            "Maduranga",
            "Wimalarathne",
            "madhuranganw@gmail.com",
            new DateOnly(1995, 03, 19),
            "+971524016331");

        // Type 1
        Person person = mapper.Map<Person>(dataDto);
        Person.ToString(person);

        // Type 2
        Person person2 = new();
        dataDto.Email = "maduranga.wimalarathne95@gmail.com";

        mapper.Map(dataDto, person2);
        Person.ToString(person2);

        // Type 3
        List<PersonDto> people = new();
        people.Add(dataDto);
        people.Add(new PersonDto("Maduranga", "NW", "namal@gmail.com", new DateOnly(1995, 03, 19), "1234567890"));

        var peopleList = mapper.Map<IEnumerable<Person>>(people);
        peopleList.ToList().ForEach(p => Console.WriteLine($"{p.FullName}, {p.Email}"));
    }

    #region Pattern Matching

    public static void PrintPatternMatchingStatements()
    {
        // Type pattern
        string name = "Anonymous";
        object obj = name;

        if (obj is string str)
        {
            Console.WriteLine($"Object is a string of length: {str.Length}");
        }

        // Property pattern
        Employee employee = new(1, "John Doe", "IT", 60000, DateTime.Now.AddYears(-3));

        if (employee is { Department: "IT", Salary: > 50000 })
        {
            Console.WriteLine("Employee works in IT and earns more than 50,000");
        }

        // Tuple pattern
        (int x, int y) coordinates = (5, 0); // Tuple declaration

        var result = (coordinates) switch
        {
            (0, 0) => "Origin",
            (var a, 0) => $"X-axis at {a}",
            (0, var b) => $"Y-axis at {b}",
            _ => "Elsewhere"
        };

        Console.WriteLine(result);

        // Positional pattern
        Point p = new(0, 7);
        Console.WriteLine(Classify(p));

        // Logical pattern
        if (employee is Employee and { Salary: >= 60000 })
        {
            Console.WriteLine("Employee earns at least 60,000");
        }
    }

    private static string Classify(Point point) => point switch
    {
        (0, 0) => "Origin",
        (var x, 0) => $"X-axis at {x}",
        (0, var y) => $"Y-axis at {y}",
        _ => "Elsewhere"
    };

    #endregion

    // Threads and Tasks
    public static void PrintThreadingFunctions()
    {
        //ThreadingExamples.ThreadMethod();
        //ThreadingExamples.ParameterizedThreadMethod();
        //ThreadingExamples.ThreadSynchronization();
        //ThreadingExamples.ThreadSafetyUsingMutex();
        //ThreadingExamples.ThreadSafetyUsingSemaphore();
        //ThreadingExamples.ThreadPoolExecution();
        //ThreadingExamples.BackgroundWorkWithTasks();
        //Task.WaitAll(ThreadingExamples.NonBlockingOperationsWithAsyncAwait()); // Less effective approach: Task.Run(() => ThreadingExamples.NonBlockingOperationsWithAsyncAwait()).Wait();
        //ThreadingExamples.CancellationOfTasks();
        //ThreadingExamples.ParallelProcessing();
    }

    #region Design Patterns
    public static void PrintSingletonPatternFunctions()
    {
        Thread[] threads = new Thread[2];

        for (int i = 0; i < threads.Length; i++)
        {
            int index = i;
            threads[i] = new Thread(() =>
            {
                var instance = SingletonPattern.Instance;
                Console.WriteLine($"Thread {index}: Got instance.");
            });
            threads[i].Start();
        }

        // Wait for all threads to complete
        foreach (var t in threads)
        {
            t.Join();
        }

        SingletonPattern instance1 = SingletonPattern.Instance;
        SingletonPattern instance2 = SingletonPattern.Instance;

        Console.WriteLine($"Reference equal? {instance1 == instance2}");

        // Pin and get addresses
        GCHandle handle1 = GCHandle.Alloc(instance1, GCHandleType.Pinned);
        GCHandle handle2 = GCHandle.Alloc(instance2, GCHandleType.Pinned);

        try
        {
            IntPtr addr1 = handle1.AddrOfPinnedObject();
            IntPtr addr2 = handle2.AddrOfPinnedObject();

            Console.WriteLine($"Address 1: 0x{addr1.ToInt64():X}");
            Console.WriteLine($"Address 2: 0x{addr2.ToInt64():X}");

            Console.WriteLine($"Same address? {addr1 == addr2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            handle1.Free();
            handle2.Free();
        }
    }

    public static void PrintBuilderResults()
    {
        var gamingPC = new ComputerBuilder()
            .SetCPU("Intel i9-13900K")
            .SetRAM("32GB DDR5")
            .SetStorage("2TB NVMe SSD")
            .SetGPU("NVIDIA RTX 4090")
            .SetMotherboard("ASUS ROG Maximus")
            .SetPowerSupply("1000W 80+ Gold")
            .AddWiFi()
            .AddBluetooth()
            .Build();

        Console.WriteLine("\n" + gamingPC);
    }

    public static void ExecuteStrategy()
    {
        ShoppingCart cart = new();
        cart.SetPaymentStrategy(new PaypalPayment());
        cart.Checkout(50.50m);
    }

    public static void ExecuteFactory()
    {
        var factory = new FileLoggerFactory();
        ICustomLogger logger = factory.CreateLogger();
        logger.Log("Hello!!");
    }

    #endregion

    public static void PrintTupleIllustrations()
    {
        (var x, var y) = (10, 20);
        Console.WriteLine($"X: {x}, Y: {y}");

        var person = (Name: "Randhil", Age: 28, Country: "Sri Lanka");
        var (name, age, country) = person;
        Console.WriteLine($"Name: {name}, Age: {age}, Country: {country}");

        var info = (35, "USA", true);
        Console.WriteLine($"Age: {info.Item1}, Country: {info.Item2}, Citizen: {info.Item3}");
        Console.WriteLine($"{info.Item1.GetType()}, {info.Item2.GetType()}, {info.Item3.GetType()}");

        var (a, _, c) = (1, 2, 3);
        Console.WriteLine(a + ", " + c);

        var values = new int[] { 5, 10, 3, 8, 2 };
        var (min, max, sum) = Calculate(values);
        Console.WriteLine($"Min: {min}, Max: {max}, Sum: {sum}");


        var (FirstName, LastName) = GetName();
        var (first, last) = GetName();
        Console.WriteLine($"First Name: {FirstName}/{first}, Last Name: {LastName}/{last}");

    }

    public static async Task PrintDocumentProcessedResults()
    {
        string baseDir = AppContext.BaseDirectory;
        string filePath = Path.Combine(baseDir, "Samples/Documents", "sample-text.txt");
        var wordCounts = await Concurrency.DocumentProcessor.ProcessFilesAsync(new[] { filePath }, CancellationToken.None);

        foreach (var kvp in wordCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    public static void PrintStringManipulations()
    {
        Practice.Strings.StringManipulations.ArrayStringTransformations();
        Practice.Strings.StringManipulations.EqualityANDOrdering();
    }

    public static async Task CustomExceptionHandling()
    {
        // Basic custom exception
        try
        {
            var registration = new UserRegistration();
            registration.RegisterUser("John", 15);
        }
        catch (InvalidAgeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Custom exception with properties
        try
        {
            var account = new BankAccount { AccountNumber = "ACC123", Balance = 100 };
            account.Withdraw(200);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Generic exception
        try
        {
            var personRepo = new Repository<Person>();
            personRepo.GetById("pid123");
        }
        catch (ResourceNotFoundException<Person> ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Database exception
        var dbService = new Practice.Database.DatabaseService(
            "Server=localhost;Database=MyDB;Trusted_Connection=true;",
            "MyDB");

        try
        {
            await dbService.ExecuteQueryAsync("INSERT INTO Users (Name) VALUES ('John')");
        }
        catch (DatabaseException ex)
        {
            Console.WriteLine($"Database Error: {ex.Message}");
            Console.WriteLine($"Database: {ex.DatabaseName}");
            Console.WriteLine($"Failed Query: {ex.Query}");
            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
        }
    }

    #region Helpers
    private static (string FirstName, string LastName) GetName() => ("Madur", "Wim");

    private static (int, int, int) Calculate(int[] values) => (values.Min(), values.Max(), values.Sum());
    #endregion
}

// reference type