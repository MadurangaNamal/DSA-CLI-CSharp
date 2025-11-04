namespace practice.practiceFiles;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
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

    public override string ToString()
    {
        return $"Employee[Id={Id}, Name={Name}, Department={Department}, Salary={Salary:C}, JoinDate={JoinDate:d}]";
    }
}

public static class BusinessProcess
{
    private static readonly string[] Departments = { "IT", "HR", "Finance", "Marketing", "Operations" };

    private static readonly string[] FirstNames = { "John", "Jane", "Robert", "Emily", "Michael", "Sarah", "David", "Lisa", "James", "Emma" };

    private static readonly string[] LastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson" };

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

            employees.Add(new Employee(i, $"{firstName} {lastName}", Departments[random.Next(Departments.Length)], (decimal)salary, joinDate));
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
