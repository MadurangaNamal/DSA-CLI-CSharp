using MediatR;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.EfCore.Entites;

namespace Practice.DesignPatterns.Mediator.Employees;

public class CreateEmployeeHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateEmployeeCommand, int>
{
    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken ct)
    {
        var department = await dbContext.Departments.FindAsync(request.DepartmentId, ct);

        if (department == null)
            throw new KeyNotFoundException($"Department {request.DepartmentId} not found.");

        var profile = await dbContext.Profiles
            .FirstOrDefaultAsync(p => EF.Functions.ILike(p.Email, request.Email), ct); // Ignores case. Postgres only.

        if (profile != null)
            throw new InvalidOperationException($"Email '{request.Email}' is already in use.");

        var titles = (request.SkillTitles ?? [])
            .Select(t => t.Trim())
            .Where(t => t.Length > 0)
            .Distinct()
            .ToList();

        // Reuse existing skills, create the missing ones
        var skills = await dbContext.Skills.Where(s => titles.Contains(s.Title)).ToListAsync(ct);

        skills.AddRange(titles
            .Except(skills.Select(s => s.Title))
            .Select(t => new Skill { Title = t }));

        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            JoinedDate = DateTime.SpecifyKind(request.JoinedDate, DateTimeKind.Utc),
            DepartmentId = request.DepartmentId,
            Profile = new EmployeeProfile { Email = request.Email },
            Skills = skills
        };

        dbContext.Employees.Add(employee);
        await dbContext.SaveChangesAsync(ct);

        return employee.Id;
    }
}
