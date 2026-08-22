namespace Practice.EfCore.Models;

public class Skill
{
    public int Id { get; set; }
    public required string Title { get; set; }


    // Collection navigation property for the many-to-many relationship with Employee
    public List<Employee> Employees { get; set; } = new();
}
