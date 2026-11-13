using MediatR;
using Practice.Models;

namespace Practice.DesignPatterns.Mediator.Employees;

public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto?>;
