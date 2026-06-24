using Microsoft.EntityFrameworkCore;
using Practice.Entities;

namespace Practice.Data;

public static class SeedData
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        if (await context.Orders.AnyAsync())
        {
            Console.WriteLine("Database already has data. Skipping seed..");
            return;
        }

        var random = new Random();
        var countries = new string[] { "Sri Lanka", "UK", "UAE", "Pakistan", "Malaysia" };

        var customers = Enumerable.Range(1, 1_000_000)
            .Select(i => new Customer
            {
                Name = $"Customer {i}",
                Country = countries[random.Next(countries.Length)],
                CreatedAt = DateTime.UtcNow.AddDays(-random.Next(365))
            }).ToList();

        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();

        var orders = new List<Order>();
        random = new Random();

        for (int i = 1; i <= 1_000_000; i++)
        {
            orders.Add(new Order
            {
                CustomerId = customers[random.Next(customers.Count)].Id,
                Amount = random.Next(100, 10000),
                Status = i % 2 == 0 ? "Completed" : "Pending",
                CreatedAt = DateTime.UtcNow.AddDays(-random.Next(100))
            });
        }

        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();
    }
}
