using Practice.Data;
using Practice.Models;
using System.Linq.Dynamic.Core;

namespace Practice.Demos;

public static class LinqExercises
{
    private static readonly List<Employee> employees = EmployeeDataFactory.GenerateEmployeesList();
    private static readonly List<Department> departments = EmployeeDataFactory.GenerateDepartmentsList();

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
}
