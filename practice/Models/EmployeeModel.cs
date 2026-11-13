namespace Practice.Models;

public class EmployeeModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public DateTime JoinDate { get; set; }

    public EmployeeModel(int id, string name, string department, decimal salary, DateTime joinDate)
    {
        Id = id;
        Name = name;
        Department = department;
        Salary = salary;
        JoinDate = joinDate;
    }

    public EmployeeModel(int id, string name, int departmentId, string department, decimal salary, DateTime joinDate)
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
