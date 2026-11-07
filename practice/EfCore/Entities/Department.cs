namespace Practice.EfCore.Entites;

public class Department
{
    public int Id { get; set; }
    public required string Name { get; set; }

    // Collection navigation property for the one-to-many relationship with Employee
    public ICollection<Employee> Employees { get; set; } = [];
}
