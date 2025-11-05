namespace practice.practiceFiles.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string ManagerName { get; set; }
    public decimal Budget { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }

    public Department()
    {
        Name = string.Empty;
        Location = string.Empty;
        ManagerName = string.Empty;
        Description = string.Empty;
    }

    public Department(int id, string name, string location, string managerName, decimal budget)
    {
        Id = id;
        Name = name;
        Location = location;
        ManagerName = managerName;
        Budget = budget;
        Description = $"{name} Department";
        CreatedDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Department {Id}: {Name} (Location: {Location}, Manager: {ManagerName}, Budget: ${Budget:N2})";
    }
}
