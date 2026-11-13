using MediatR;

namespace Practice.DesignPatterns.Mediator.Employees;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    DateTime JoinedDate,
    int DepartmentId,
    string Email,
    List<string>? SkillTitles = null) : IRequest<int>;

