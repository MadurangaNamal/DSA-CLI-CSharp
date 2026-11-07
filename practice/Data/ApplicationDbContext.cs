using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Practice.EfCore.Entites;

namespace Practice.Data;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    #region Bulk data manipulation & Transcation demos

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Item> Items { get; set; }

    #endregion

    #region Mapping options

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<EmployeeProfile> Profiles { get; set; }
    public DbSet<Skill> Skills { get; set; }

    #endregion

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Supplier)
            .WithMany(s => s.Items)
            .HasForeignKey(i => i.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        ////////////////////////////////////////

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Profile)
            .WithOne(p => p.Employee)
            .HasForeignKey<EmployeeProfile>(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Skills)
            .WithMany(s => s.Employees)
            .UsingEntity<Dictionary<string, object>>(
                "EmployeeSkills",
                j => j.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                j => j.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId")
            );

        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);
        });

        ////

        modelBuilder.Entity<EmployeeProfile>(entity =>
        {
            entity.HasKey(ep => ep.Id);

            entity.Property(ep => ep.Email)
            .IsRequired();

            entity.HasIndex(ep => ep.Email)
            .IsUnique();
        });

        ////

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(100);
        });

        base.OnModelCreating(modelBuilder);
    }
}
