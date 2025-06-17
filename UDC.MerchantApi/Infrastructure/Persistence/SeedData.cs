using Microsoft.EntityFrameworkCore;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task EnsureSeededAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        await context.Database.MigrateAsync();
        
        if (await context.Merchants.AnyAsync()) return;

        var now = DateTime.UtcNow;

        var merchants = new List<Merchant>
        {
            new()
            {
                Name = "Retail Starter",
                Email = "retail@example.com",
                Category = Category.Retail,
                CreatedAt = now
            },
            new()
            {
                Name = "Food Starter",
                Email = "food@example.com",
                Category = Category.Food,
                CreatedAt = now
            },
            new()
            {
                Name = "Service Starter",
                Email = "services@example.com",
                Category = Category.Services,
                CreatedAt = now
            }
        };

        await context.Merchants.AddRangeAsync(merchants);
        await context.SaveChangesAsync();
    }
}