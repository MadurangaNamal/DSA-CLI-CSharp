using Microsoft.EntityFrameworkCore;
using Practice.EfCore.Models;

namespace Practice.Data;

public static class SeedEmployeeData
{
    public static async Task Initialize()
    {
        await using var dbContext = new ApplicationDbContext();

        if (await dbContext.Employees.AnyAsync())
        {
            return;
        }

        var departments = new List<Department>
        {
            new() { Name = "Engineering" },
            new() { Name = "Human Resources" },
            new() { Name = "Finance" }
        };

        dbContext.Departments.AddRange(departments);

        var skills = new List<Skill>
        {
            new() { Title = "C#" },
            new() { Title = "ASP.NET Core" },
            new() { Title = "SQL Server" },
            new() { Title = "React" },
            new() { Title = "Docker" }
        };

        dbContext.Skills.AddRange(skills);

        await dbContext.SaveChangesAsync();
        var employees = new List<Employee>
        {
            new()
            {
                FirstName = "John",
                LastName = "Smith",
                JoinedDate = new DateTime(
                    2023, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                Department = departments[0],
                Skills = new List<Skill>
                {
                    skills[0], skills[1], skills[2]
                }
            },
            new()
            {
                FirstName = "Sarah",
                LastName = "Johnson",
                JoinedDate = new DateTime(
                    2022, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                Department = departments[0],
                Skills = new List<Skill>
                {
                    skills[0], skills[1], skills[3], skills[4]
                }
            },
            new()
            {
                FirstName = "David",
                LastName = "Brown",
                JoinedDate = new DateTime(
                    2024, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                Department = departments[1],
                Skills = new List<Skill>
                {
                    skills[2], skills[3]
                }
            },
            new()
            {
                FirstName = "Emily",
                LastName = "Davis",
                JoinedDate = new DateTime(
                    2021, 9, 20, 0, 0, 0, DateTimeKind.Utc),
                Department = departments[2],
                Skills = new List<Skill>
                {
                    skills[0], skills[2]
                }
            }
        };

        dbContext.Employees.AddRange(employees);
        var profiles = new List<EmployeeProfile>
        {
            new()
            {
                Email = "john.smith@example.com",
                Phone = "+971501234567",
                Employee = employees[0]
            },
            new()
            {
                Email = "sarah.johnson@example.com",
                Phone = "+971502345678",
                Employee = employees[1]
            },
            new()
            {
                Email = "david.brown@example.com",
                Phone = "+971503456789",
                Employee = employees[2]
            },
            new()
            {
                Email = "emily.davis@example.com",
                Phone = "+971504567890",
                Employee = employees[3]
            }
        };

        dbContext.Profiles.AddRange(profiles);
        await dbContext.SaveChangesAsync();
    }
}
