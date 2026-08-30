using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Practice.Data;
using Practice.EfCore.Models;

namespace Practice.EfCore.Repository;

public class EmployeeRepository : IRepository<Employee>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<EmployeeRepository> _logger;

    public EmployeeRepository()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        _logger = loggerFactory.CreateLogger<EmployeeRepository>();
        _dbContext = new ApplicationDbContext();
    }

    public async Task AddAsync(Employee entity)
    {
        var response = entity != null ?
            await _dbContext.Employees.AddAsync(entity) : throw new ArgumentNullException(nameof(entity));

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Employee added: {EmployeeId}", response.Entity.Id);
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);

        if (employee == null)
        {
            _logger.LogWarning("Employee with ID {EmployeeId} not found for deletion.", id);
            throw new KeyNotFoundException($"Employee with ID {id} not found.");
        }

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Employee deleted: {EmployeeId}", id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .AnyAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .ToListAsync() ?? [];
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .Include(e => e.Profile)
            .Include(e => e.Skills)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task UpdateAsync(int id, Employee entity)
    {
        var employee = await _dbContext.Employees.FindAsync(id);

        if (employee == null)
        {
            _logger.LogWarning("Employee with ID {EmployeeId} not found for deletion.", id);
            throw new KeyNotFoundException($"Employee with ID {id} not found.");
        }

        employee.FirstName = entity.FirstName;
        employee.LastName = entity.LastName;
        employee.JoinedDate = entity.JoinedDate;
        employee.DepartmentId = entity.DepartmentId;

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Employee updated: {EmployeeId}", id);
    }
}
