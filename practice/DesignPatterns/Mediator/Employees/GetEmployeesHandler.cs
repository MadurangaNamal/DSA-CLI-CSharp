using MediatR;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;

namespace Practice.DesignPatterns.Mediator.Employees;

public class GetEmployeesHandler(ApplicationDbContext db)
    : IRequestHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>
{
    public async Task<PagedResult<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken ct)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var query = db.Employees.AsNoTracking().AsQueryable();

        if (request.DepartmentId is { } deptId)
            query = query.Where(e => e.DepartmentId == deptId);

        if (!string.IsNullOrWhiteSpace(request.Skill))
            query = query.Where(e => e.Skills.Any(s => s.Title == request.Skill));

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(e =>
                EF.Functions.ILike(e.FirstName + " " + e.LastName, $"%{request.Search}%"));

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ThenBy(e => e.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToDto()
            .ToListAsync(ct);

        return new PagedResult<EmployeeDto>(items, total, page, pageSize);
    }
}