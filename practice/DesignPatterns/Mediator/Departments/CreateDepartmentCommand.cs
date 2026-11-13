using MediatR;

namespace Practice.DesignPatterns.Mediator.Departments;

public record CreateDepartmentCommand(string Name) : IRequest<int>;
