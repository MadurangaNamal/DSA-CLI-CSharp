using Practice.Models;

namespace Practice.Data;

/// <summary>
/// Generates sample employee and department data for LINQ and collection demos.
/// </summary>
public static class EmployeeDataFactory
{
    private static readonly string[] FirstNames = { "John", "Jane", "Robert", "Emily", "Michael",
        "Sarah", "David", "Lisa", "James", "Emma" };
    private static readonly string[] LastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones",
        "Miller", "Davis", "Garcia", "Rodriguez", "Wilson" };
    private static readonly (int Id, string Name)[] Departments = {
        (1, "IT"),
        (2, "HR"),
        (3, "Finance"),
        (4, "Marketing"),
        (5, "Operations")
    };

    public static List<Department> GenerateDepartmentsList()
    {
        return Departments.Select(d => new Department
        {
            Id = d.Id,
            Name = d.Name,
            Description = $"{d.Name} Department",
            CreatedDate = DateTime.Now.AddYears(-8).AddDays(d.Id)
        }).ToList();
    }

    public static List<Employee> GenerateEmployeesList(int count = 50)
    {
        var random = new Random();
        var employees = new List<Employee>();
        var startDate = new DateTime(2015, 1, 1);
        var endDate = DateTime.Now;

        for (int i = 1; i <= count; i++)
        {
            var firstName = FirstNames[random.Next(FirstNames.Length)];
            var lastName = LastNames[random.Next(LastNames.Length)];
            var joinDate = startDate.AddDays(random.Next((endDate - startDate).Days));
            var salary = Math.Round(30000 + random.NextDouble() * 70000, 2);
            var department = Departments[random.Next(Departments.Length)];

            employees.Add(new Employee(
                id: i,
                name: $"{firstName} {lastName}",
                departmentId: department.Id,
                department: department.Name,
                salary: (decimal)salary,
                joinDate: joinDate));
        }

        return employees;
    }

    public static IEnumerable<Employee> GetEmployeesEnumerable() =>
        GenerateEmployeesList(20).AsEnumerable();

    public static ICollection<Employee> GetEmployeeCollection() =>
        GenerateEmployeesList(25);
}
