namespace Practice.EfCore.Models;

public class Employee
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime JoinedDate { get; set; }


    // Required foreign key for the one-to-many relationship with Department
    public int DepartmentId { get; set; }

    // Required reference navigation for the one-to-many relationship with Department
    public Department Department { get; set; } = null!;

    /*
     // Optional One-to-many relationship with Department

        public int? DepartmentId  {   get;set; }
        public Department? Department {  get; set; }
    */

    /////

    // Reference navigation for the one-to-one relationship with EmployeeProfile
    public EmployeeProfile? Profile { get; set; }

    /////

    // Collection navigation for the many-to-many relationship with Skill
    public List<Skill> Skills { get; set; } = new();

}
