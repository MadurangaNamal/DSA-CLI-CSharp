using MediatR;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;

namespace Practice.DesignPatterns.Mediator.Employees;

public class GetEmployeeByIdHandler(ApplicationDbContext db)
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    public Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken ct) => db.Employees
             .AsNoTracking()
             .Where(e => e.Id == request.Id)
             .ToDto()
             .FirstOrDefaultAsync(ct);
}
