using System.ComponentModel.DataAnnotations;

namespace Practice.EfCore.Models;

public class EmployeeProfile
{
    public int Id { get; set; }

    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string? Phone { get; set; }

    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public required string Email { get; set; }


    // Required foreign key for the one-to-one relationship with Employee
    public int EmployeeId { get; set; }

    //Required reference navigation for the one-to-one relationship with Employee
    public Employee Employee { get; set; } = null!;
}
