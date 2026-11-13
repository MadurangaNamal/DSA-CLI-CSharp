using MediatR;
using Practice.Data;
using Practice.EfCore.Entites;

namespace Practice.DesignPatterns.Mediator.Departments;

public class CreateDepartmentHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateDepartmentCommand, int>
{
    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken ct)
    {
        var dept = new Department
        {
            Name = request.Name
        };

        dbContext.Departments.Add(dept);

        await dbContext.SaveChangesAsync(ct);
        return dept.Id;
    }
}
