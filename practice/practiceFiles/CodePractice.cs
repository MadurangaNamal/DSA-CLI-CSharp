using AutoMapper;
using System.Linq.Dynamic.Core;

namespace practice.practiceFiles;

public class CodePractice
{
    //Find second largest
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

    //Binary search
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

    //Merge two sorted linked lists into one sorted list
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

    //Task Progress Reporting
    public static async Task ProcessDataAsync(string[] departments, IProgress<int> progress)
    {
        for (int i = 1; i <= departments.Length; i++)
        {
            progress.Report(i);
            await Task.Delay(1000);
        }

        Console.WriteLine("Done processing data");
    }

    //Helper methods
    public static void PrintList(ListNode? head)
    {
        while (head != null)
        {
            Console.Write(head.Value + " -> ");
            head = head.Next!;
        }
        Console.WriteLine("null");
    }

    //Using in built LinkedList
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

    //Automapper
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
    }

}


