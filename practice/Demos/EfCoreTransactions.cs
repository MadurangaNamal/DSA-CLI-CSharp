using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Practice.Data;
using Practice.Entities;

namespace Practice.Demos;

public class EfCoreTransactions
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<EfCoreTransactions> _logger;

    public EfCoreTransactions(ApplicationDbContext dbContext, ILogger<EfCoreTransactions> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger;
    }

    public async Task RunAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation("Starting transaction...");

            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                Name = "New Supplier",
                Phone = "0524567654",
                Address = "123 Main St, Al bharsha"
            };

            _dbContext.Suppliers.Add(supplier);
            await _dbContext.SaveChangesAsync();

            var count = await _dbContext.Items.CountAsync();

            var itemId = count == 0 ? 1 : count + 1;
            var itemName = "Biscoff Biscuit";
            var itemPrice = 6.99;
            var itemCode = 6474;

            _logger.LogInformation("Inserting item: {ItemName}", itemName);

            await _dbContext.Database.ExecuteSqlRawAsync(
                @"INSERT INTO ""Items"" (""Id"", ""Name"", ""Price"", ""Code"", ""SupplierId"") VALUES ({0}, {1}, {2}, {3}, {4})",
                itemId, itemName, itemPrice, itemCode, supplier.Id);

            await transaction.CommitAsync();
            _logger.LogInformation("Transaction committed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during the transaction. Rolling back changes.");

            await transaction.RollbackAsync();
            throw;
        }
    }
}
