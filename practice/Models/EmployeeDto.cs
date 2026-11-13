namespace Practice.Models;

public record EmployeeDto(
    int Id,
    string FirstName,
    string LastName,
    DateTime JoinedDate,
    int DepartmentId,
    string Department,
    string? Email,
    List<string> Skills);
