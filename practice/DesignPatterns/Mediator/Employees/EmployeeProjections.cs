using Practice.EfCore.Entites;
using Practice.Models;

namespace Practice.DesignPatterns.Mediator.Employees;

internal static class EmployeeProjections
{
    public static IQueryable<EmployeeDto> ToDto(this IQueryable<Employee> q) =>
        q.Select(e => new EmployeeDto(
            e.Id,
            e.FirstName,
            e.LastName,
            e.JoinedDate,
            e.DepartmentId,
            e.Department.Name,
            e.Profile != null ? e.Profile.Email : null,
            e.Skills.Select(s => s.Title).ToList()));
}
