namespace practice.practiceFiles.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public DateTime JoinDate { get; set; }

    public Employee(int id, string name, string department, decimal salary, DateTime joinDate)
    {
        Id = id;
        Name = name;
        Department = department;
        Salary = salary;
        JoinDate = joinDate;
    }

    public Employee(int id, string name, int departmentId, string department, decimal salary, DateTime joinDate)
    {
        Id = id;
        Name = name;
        DepartmentId = departmentId;
        Department = department;
        Salary = salary;
        JoinDate = joinDate;
    }

    public override string ToString()
    {
        return $"Employee [Id={Id}, Name={Name}, Department={Department}, Salary={Salary:C}, JoinDate={JoinDate:d}]";
    }
}

public static class BusinessProcess
{
    private static readonly string[] FirstNames = { "John", "Jane", "Robert", "Emily", "Michael", "Sarah", "David", "Lisa", "James", "Emma" };
    private static readonly string[] LastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson" };
    private static readonly (int Id, string Name)[] Departments =
   {
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
                joinDate: joinDate
            ));
        }

        return employees;
    }

    public static IEnumerable<Employee> GetEmployeesEnumerable()
    {
        return GenerateEmployeesList(20).AsEnumerable();
    }

    public static ICollection<Employee> GetEmployeeCollection()
    {
        ICollection<Employee> employees = GenerateEmployeesList(25);
        return employees;
    }
}
