using AutoMapper;
using practice.practiceFiles.DesignPatterns;
using practice.practiceFiles.Models;
using System.Linq.Dynamic.Core;
using System.Runtime.InteropServices;
using System.Text;

namespace practice.practiceFiles;

public static class CodePractice
{
    private static readonly List<Employee> employees = BusinessProcess.GenerateEmployeesList();
    private static readonly List<Department> departments = BusinessProcess.GenerateDepartmentsList();

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

    // Reverse a string
    public static string ReverseString(string text)
    {
        StringBuilder reversedText = new();

        if (!string.IsNullOrEmpty(text))
        {
            char[] characters = text.ToCharArray();

            for (int i = characters.Length - 1; i >= 0; i--)
            {
                reversedText.Append(characters[i]);
            }
        }

        return reversedText.ToString();
    }

    // Find second largest
    public static int GetSecondLargestNumber(int[] itemList)
    {
        int largestNumber = int.MinValue;
        int secondLargestNumber = int.MinValue;

        foreach (int number in itemList)
        {
            if (number > largestNumber)
            {
                secondLargestNumber = largestNumber;
                largestNumber = number;
            }
            else if (number > secondLargestNumber && number != largestNumber)
            {
                secondLargestNumber = number;
            }
        }

        if (secondLargestNumber == int.MinValue)
        {
            Console.WriteLine("There is no second largest number");
        }

        return secondLargestNumber;
    }

    // Find first non-repeating character
    public static char GetFirstNonRepeatingCharacter(string phrase)
    {
        Dictionary<char, int> counts = [];

        foreach (char chtr in phrase.ToLower())
        {
            if (counts.ContainsKey(chtr))
            {
                counts[chtr]++;
            }
            else
            {
                counts.Add(chtr, 1);
            }
        }

        foreach (char c in counts.Keys)
        {
            if (counts[c] == 1)
                return c;
        }

        Console.WriteLine($"No non-repeating character found.");
        return '\0';
    }

    // Binary search
    public static int FindElementInSortedArray(int item, int[] sortedArray)
    {
        int low = 0;
        int high = sortedArray.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;

            if (sortedArray[mid] == item)
            {
                return mid;
            }
            else if (sortedArray[mid] < item)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        Console.WriteLine("Element not found");
        return -1;
    }

    // Merge two sorted linked lists into one sorted list
    /*
     *  Use a dummy node to simplify handling the head of the merged list.
        Use a pointer (current) to track the last node of the merged list.
        Compare nodes from both lists (l1 and l2), attaching the smaller one to current.
        Move current and the list pointer (l1 or l2) forward.
        If any list is not fully traversed, append the remaining part.
     * */
    public static ListNode? MergeTwoLists(ListNode l1, ListNode l2)
    {
        ListNode dummy = new(-1); // Dummy node to simplify handling
        ListNode current = dummy;

        while (l1 != null && l2 != null)
        {
            if (l1.Value < l2.Value)
            {
                current.Next = l1;
                l1 = l1.Next!;
            }
            else
            {
                current.Next = l2;
                l2 = l2.Next!;
            }
            current = current.Next;
        }

        // Attach the remaining elements
        current.Next = l1 ?? l2;

        return dummy.Next;
    }

    // Task Progress Reporting
    public static async Task ProcessDataAsync(string[] departments, IProgress<int> progress)
    {
        for (int i = 1; i <= departments.Length; i++)
        {
            progress.Report(i);
            await Task.Delay(1000);
        }

        Console.WriteLine("Done processing data");
    }

    // Helper methods
    public static void PrintList(ListNode? head)
    {
        while (head != null)
        {
            Console.Write(head.Value + " -> ");
            head = head.Next!;
        }
        Console.WriteLine("null");
    }

    // Using in built LinkedList
    public static void PrintLinkedListFunctions(string[]? values)
    {
        LinkedList<string>? linkedList = new(values!);

        linkedList.AddFirst("First Name");

        linkedList.AddLast("Last Name");

        LinkedListNode<string>? first = linkedList.First;
        LinkedListNode<string>? last = linkedList.Last;

        Console.WriteLine($"First: {first!.Value}, Last: {last!.Value}");

        linkedList.AddAfter(first, "Second Name");

        Console.WriteLine("--------------------------------------");

        foreach (string item in linkedList)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("--------------------------------------");

        Console.WriteLine(first.Next!.Value);
        Console.WriteLine(last.Previous!.Value);

        Console.WriteLine("--------------------------------------");

        Console.WriteLine(linkedList.Find("Second"));
        Console.WriteLine(linkedList.Find("Second Name"));
        Console.WriteLine(linkedList.Contains("Second Name"));
    }

    #region LINQ samples

    // Filter out employees whose IDs are in the given array
    public static List<Employee> FilterEmployees(int[] ids)
    {
        List<Employee> filteredEmployees = employees
        .Where(employee => !ids.Contains(employee.Id))
        .ToList();

        return filteredEmployees;
    }

    // Sort employees by a specified property and order
    public static List<Employee> SortEmployees(string sortProperty, bool ascending = true)
    {
        // Use reflection to dynamically get the property value for sorting
        Func<Employee, object?> keySelector = employee =>
            typeof(Employee).GetProperty(sortProperty)?.GetValue(employee, null);

        return ascending
            ? employees.OrderBy(keySelector).ToList()
            : employees.OrderByDescending(keySelector).ToList();
    }

    // Paginate employees with optional search term
    public static List<Employee> GetPaginatedEmployees(int page, int pageSize, string? searchTerm = null)
    {
        return employees
            .Where(e => e.Name.Contains(searchTerm!, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(searchTerm))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    // Get department-wise statistics
    // dynamic type is used here to return an anonymous type (resolved on runtime)
    public static dynamic GetDepartmentStats()
    {
        return employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                AvgSalary = g.Average(e => e.Salary),
                TotalEmployees = g.Count(),
                MaxSalary = g.Max(e => e.Salary)
            })
            .ToList();
    }

    // Get employees who have been with the company for more than 'n' years
    public static List<Employee> GetLongTermEmployees(int years)
    {
        var cutoffDate = DateTime.Now.AddYears(-years);

        return employees
            .Where(e => e.JoinDate <= cutoffDate)
            .OrderBy(e => e.JoinDate)
            .ToList();
    }

    // Filter employees by salary range
    public static List<Employee> FilterBySalaryRange(decimal? minSalary, decimal? maxSalary)
    {
        var query = employees.AsQueryable();

        if (minSalary.HasValue)
            query = query.Where(e => e.Salary >= minSalary.Value);

        if (maxSalary.HasValue)
            query = query.Where(e => e.Salary <= maxSalary.Value);

        return query.ToList();
    }

    // Find possible duplicate employees based on Name and Department
    public static List<Employee> FindPossibleDuplicates()
    {
        return employees
            .GroupBy(e => new { e.Name, e.Department })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .ToList();
    }

    // Get employees who joined in the last 'n' months
    public static List<Employee> GetRecentJoinees(int months)
    {
        var cutoffDate = DateTime.Now.AddMonths(-months);

        return employees
            .Where(e => e.JoinDate >= cutoffDate)
            .OrderByDescending(e => e.JoinDate)
            .ToList();
    }

    // Dynamic field selection using System.Linq.Dynamic.Core
    public static dynamic GetEmployeeOverview(string fields)
    {
        return employees
            .AsQueryable()
            .Select($"new ({fields})")
            .ToDynamicList();
    }

    // Calculate total salary budget per department
    public static Dictionary<string, decimal> GetDepartmentBudgets()
    {
        return employees
            .GroupBy(e => e.Department)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(e => e.Salary));
    }

    // Categorize employees based on salary tiers
    public static dynamic CategorizeSalaries()
    {
        return employees
            .Select(e => new
            {
                e.Name,
                SalaryTier = e.Salary switch
                {
                    > 100000 => "Executive",
                    > 75000 => "Senior",
                    > 50000 => "Mid",
                    _ => "Junior"
                }
            })
            .GroupBy(e => e.SalaryTier)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    // Generic method to print employees list
    public static void PrintEmployeesList(List<Employee>? employeesList = null)
    {
        employeesList = employeesList ?? employees;

        employeesList.ForEach(employee =>
        {
            Console.WriteLine($"{employee.Id}, {employee.Name}, {employee.Department}, {employee.Salary}, {employee.JoinDate}");
        });
    }

    /*
     *  More LINQ
     *  SQL equivalant query samples
     */
    public static void PrintCustomLinqQueryResults()
    {
        /*
         * >> SQL 
         * SELECT * FROM Employees
         */

        var allEmployees = employees.ToList();
        allEmployees.ForEach(employee => Console.WriteLine($"{employee.Id}, {employee.Name}, {employee.Department}"));

        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT Name, Salary 
         * FROM Employees
         */

        var emps = employees.Select(e => new
        {
            e.Name,
            e.Salary
        });

        foreach (var item in emps)
        {
            Console.WriteLine($"Name: {item.Name}, Salary: {item.Salary}");
        }

        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT * FROM Employees 
         * WHERE Salary > 80000
         */

        var emps2 = employees.Where(emp => emp.Salary > 80000);

        PrintEmployeesEnumerable(emps2);
        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT TOP 3 * FROM Employees
         */

        var emps3 = employees.Take(3);

        PrintEmployeesEnumerable(emps3);
        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT * 
         * FROM Employees
         * Order By name
         */

        var emps4 = employees
            .OrderBy(emp => emp.Name);

        PrintEmployeesEnumerable(emps4);
        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT TOP 3 * 
         * FROM Employees
         * Order By salary desc
         */

        var emps5 = employees
            .OrderByDescending(emp => emp.Salary)
            .Take(3);

        PrintEmployeesEnumerable(emps5);
        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT * FROM Employees 
         * WHERE Department = 'IT' AND Salary > 60000
         */

        var emps6 = employees
            .Where(emp => emp.Department == "IT" && emp.Salary > 60000);

        PrintEmployeesEnumerable(emps6);
        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT * FROM Employees 
         * WHERE Department = 'HR' OR Department = 'Finance';
         */

        var emps7 = employees
            .Where(emp => emp.Department == "HR" || emp.Department == "Finance");

        PrintEmployeesEnumerable(emps7);
        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT e.Name, d.DepartmentName 
         * FROM Employees e INNER JOIN Departments d 
         * ON e.DepartmentId = d.Id
         */

        var emps8 = employees.Join(departments,
            e => e.DepartmentId,
            d => d.Id,
            (e, d) => new
            {
                EmployeeName = e.Name,
                DepartmentName = d.Name
            });

        foreach (var item in emps8)
        {
            Console.WriteLine($"Name: {item.EmployeeName}, Department: {item.DepartmentName}");
        }

        Console.WriteLine('\n');

        /*
        * >> SQL 
        * SELECT e.Name, d.DepartmentName 
        * FROM Employees e LEFT JOIN Departments d 
        * ON e.DepartmentId = d.Id; 
        */

        var emps9 = employees.GroupJoin(departments,
                            e => e.DepartmentId,
                            d => d.Id,
                            (e, dGroup) => new { e, dGroup })
                        .SelectMany(
                            x => x.dGroup.DefaultIfEmpty(),
                            (e, d) => new
                            {
                                Name = e.e.Name,
                                DepartmentName = d != null ? d.Name : null
                            });

        foreach (var item in emps9)
        {
            Console.WriteLine($"Name: {item.Name}, Department: {item.DepartmentName}");
        }

        Console.WriteLine('\n');

        // Right join 

        departments.Add(new Department(6, "Sales", "Dubai", "Mngr", 50000.0m));

        var emps91 = departments.GroupJoin(employees,
                            d => d.Id,
                            e => e.DepartmentId,
                            (d, eGroup) => new { d, eGroup })
                        .SelectMany(
                            x => x.eGroup.DefaultIfEmpty(),
                            (d, e) => new
                            {
                                Name = e != null ? e.Name : null,
                                DepartmentName = d.d.Name
                            });
        foreach (var item in emps91)
        {
            Console.WriteLine($"Name: {item.Name}, Department: {item.DepartmentName}");
        }

        departments.RemoveAt(departments.Count - 1);
        Console.WriteLine('\n');

        // Aggregation

        /*
         * >> SQL 
         * SELECT Department, COUNT(*) 
         * FROM Employees 
         * GROUP BY Department;
         */

        var empsx = employees.GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                Count = g.Count()
            });

        foreach (var emp in empsx)
        {
            Console.WriteLine($"Department: {emp.Department}, Count: {emp.Count}");
        }

        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT Department, AVG(Salary) 
         * FROM Employees 
         * GROUP BY Department;
         */

        var empsxi = employees.GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                AverageSalary = g.Average(e => e.Salary)
            });

        foreach (var emp in empsxi)
        {
            Console.WriteLine($"Department: {emp.Department}, Average Salary: {emp.AverageSalary.ToString("N2")}");
        }

        Console.WriteLine('\n');

        /*
         * >> SQL 
         * SELECT MAX(Salary) 
         * FROM Employees;
         */

        var empsxii = employees.Max(e => e.Salary);

        Console.WriteLine($"Maximum salary: AED {empsxii:N2}");

        /*
         * >> SQL 
         * SELECT MIN(Salary) 
         * FROM Employees;
         */

        var empsxiii = employees.Min(e => e.Salary);

        Console.WriteLine($"Minimum salary: AED {empsxiii:N2}");

        /*
         * >> SQL 
         * SELECT SUM(Salary) 
         * FROM Employees;
         */

        var empsxiv = employees.Sum(e => e.Salary);

        Console.WriteLine($"Salary Total: AED {empsxiv:N2}");

        /*
         * >> SQL 
         * SELECT Count(*) 
         * FROM Employees;
         */

        var empsxv = employees.Count();

        Console.WriteLine($"Total Employees: {empsxv}");

    }

    private static void PrintEmployeesEnumerable(IEnumerable<Employee>? employeesList = null)
    {
        employeesList ??= [];

        foreach (var emp in employeesList)
        {
            Console.WriteLine(emp.ToString());
        }
    }

    #endregion

    // Automapper
    public static void PrintAutomapperFunctions()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<PersonProfile>());
        var mapper = config.CreateMapper();

        PersonDto dataDto = new(
            "Maduranga",
            "Wimalarathne",
            "madhuranganw@gmail.com",
            new DateOnly(1995, 03, 19),
            "+971524016331");

        //Type 1
        Person person = mapper.Map<Person>(dataDto);
        Person.ToString(person);

        //Type 2
        Person person2 = new();
        dataDto.Email = "maduranga.wimalarathne95@gmail.com";

        mapper.Map(dataDto, person2);
        Person.ToString(person2);

        //Type 3
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
        //CustomThreads.ThreadMethod();
        //CustomThreads.ParameterizedThreadMethod();
        //CustomThreads.ThreadSynchronization();
        //CustomThreads.ThreadSafetyUsingMutex();
        //CustomThreads.ThreadSafetyUsingSemaphore();
        //CustomThreads.ThreadPoolExecution();
        //CustomThreads.BackgroundWorkWithTasks();
        //Task.WaitAll(CustomThreads.NonBlockingOperationsWithAsyncAwait()); // Less effective approach: Task.Run(() => CustomThreads.NonBlockingOperationsWithAsyncAwait()).Wait();
        //CustomThreads.CancellationOfTasks();
        //CustomThreads.ParallelProcessing();
    }

    // Design Patterns
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

    public static void PrintTupleIllustrations()
    {

    }
}

// reference type
public class SampleClass
{
    public int a;
    public bool b;
    public string? value;
}

// value type
public struct SampleStruct
{
    public int a;
    public bool b;
    public string value;
}

// immutable reference type
public record SampleRecord(int a, bool b, string value);

/////////////////////////////////////////////////////////////
public record Point(int X, int Y);

////////////////////////////////////////////////////////////

public abstract record Shape(bool IsRound);
public record Circle(decimal Radius) : Shape(true);
public record Rectangle(int Length, int Wdith) : Shape(false);

