using MediatR;
using Practice.Models;

namespace Practice.DesignPatterns.Mediator.Employees;

public record GetEmployeesQuery(
    int? DepartmentId = null,
    string? Skill = null,
    string? Search = null,
    int Page = 1,
    int PageSize = 20) : IRequest<PagedResult<EmployeeDto>>;