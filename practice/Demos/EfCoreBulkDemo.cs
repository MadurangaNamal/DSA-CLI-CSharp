using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Entities;

namespace Practice.Demos;

public class EfCoreBulkDemo
{
    public async Task RunAsync()
    {
        Console.WriteLine("Running EF Core Bulk large dataset retrieval (Will take few minutes...)");
        Console.WriteLine("================================================================");

        await using var dbContext = new ApplicationDbContext();

        await dbContext.Database.EnsureCreatedAsync();
        await SeedData.Initialize(dbContext);

        Console.WriteLine("Loading sample data...");

        var orderIds = await dbContext.Orders
            .OrderBy(x => x.Id)
            .Take(50_000)
            .Select(x => x.Id)
            .ToListAsync();

        var smallOrderIds = await dbContext.Orders
            .OrderBy(x => x.Id)
            .Take(2_000)
            .Select(x => x.Id)
            .ToListAsync();

        Console.WriteLine($"Loaded {orderIds.Count} large IDs and {smallOrderIds.Count} small IDs for testing.\n");

        var result1 = await dbContext.Orders
            .Where(x => smallOrderIds.Contains(x.Id))
            .ToListAsync();

        Console.WriteLine($"Total records: {result1.Count}\n");

        var result2 = await dbContext.Orders
            .WhereBulkContains(orderIds, x => x.Id)
            .ToListAsync();

        Console.WriteLine($"Total records: {result2.Count}\n");

        var result3 = await dbContext.Orders
            .WhereBulkNotContains(orderIds, x => x.Id)
            .ToListAsync();

        Console.WriteLine($"Total records: {result3.Count}\n");

        var inputOrders = orderIds.Select(id => new Order { Id = id }).ToList();
        var result4 = await dbContext.Orders
            .BulkReadAsync(inputOrders, x => x.Id);

        Console.WriteLine($"Total records: {result4.Count}\n");

        var result5 = dbContext.Orders
            .WhereBulkContainsFilterList(smallOrderIds, x => x.Id)
            .ToList();

        Console.WriteLine($"Total records: {result5.Count}\n");

        var testIds = new List<int> { 1, 999999, 1_000_000, 2_000_000 };

        var result6 = dbContext.Orders
            .WhereBulkNotContainsFilterList(testIds, x => x.Id)
            .ToList();

        Console.WriteLine($"Items not in DB: {result6.Count}\n");

        Console.WriteLine("Completed!!");
        Console.WriteLine("================================================================");
    }
}
