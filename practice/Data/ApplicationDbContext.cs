using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Practice.Entities;

namespace Practice.Data;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        _connectionString = config.GetConnectionString("PostgresConnection")
            ?? throw new InvalidOperationException("Connection string not found");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}
