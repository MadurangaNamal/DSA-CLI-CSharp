using AutoMapper;
using System.Linq.Dynamic.Core;
using System.Text;

namespace practice.practiceFiles;

public class CodePractice
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

    public static List<Employee> FilterEmployees(int[] ids)
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        List<Employee> filteredEmployees = employees
        .Where(employee => !ids.Contains(employee.Id))
        .ToList();

        return filteredEmployees;
    }

    public static List<Employee> SortEmployees(string sortProperty, bool ascending = true)
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        // Use reflection to dynamically get the property value for sorting
        Func<Employee, object?> keySelector = employee =>
            typeof(Employee).GetProperty(sortProperty)?.GetValue(employee, null);

        return ascending
            ? employees.OrderBy(keySelector).ToList()
            : employees.OrderByDescending(keySelector).ToList();
    }

    public static List<Employee> GetPaginatedEmployees(int page, int pageSize, string searchTerm)
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        return employees
            .Where(e => e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public static dynamic GetDepartmentStats()
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

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

    public static List<Employee> GetLongTermEmployees(int years)
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        var cutoffDate = DateTime.Now.AddYears(-years);

        return employees
            .Where(e => e.JoinDate <= cutoffDate)
            .OrderBy(e => e.JoinDate)
            .ToList();
    }
    public static List<Employee> FilterBySalaryRange(decimal? minSalary, decimal? maxSalary)
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        var query = employees.AsQueryable();

        if (minSalary.HasValue)
            query = query.Where(e => e.Salary >= minSalary.Value);

        if (maxSalary.HasValue)
            query = query.Where(e => e.Salary <= maxSalary.Value);

        return query.ToList();
    }

    public static List<Employee> FindPossibleDuplicates()
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        return employees
            .GroupBy(e => new { e.Name, e.Department })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .ToList();
    }

    public static List<Employee> GetRecentJoinees(int months)
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        var cutoffDate = DateTime.Now.AddMonths(-months);

        return employees
            .Where(e => e.JoinDate >= cutoffDate)
            .OrderByDescending(e => e.JoinDate)
            .ToList();
    }

    public static dynamic GetEmployeeOverview(string fields)
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        return employees
            .AsQueryable()
            .Select($"new ({fields})")
            .ToDynamicList();
    }

    public static Dictionary<string, decimal> GetDepartmentBudgets()
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

        return employees
            .GroupBy(e => e.Department)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(e => e.Salary));
    }

    public static dynamic CategorizeSalaries()
    {
        List<Employee> employees = BusinessProcess.GenerateEmployeesList();

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

    // Threads
    public static void PrintThreadingFunctions()
    {
        //CustomThreads.ThreadMethod();
        //CustomThreads.ParameterizedThreadMethod();
        //CustomThreads.ThreadSynchronization();
        //CustomThreads.ThreadSafetyUsingMutex();
        //CustomThreads.ThreadSafetyUsingSemaphore();
        CustomThreads.ThreadPoolExecution();

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

