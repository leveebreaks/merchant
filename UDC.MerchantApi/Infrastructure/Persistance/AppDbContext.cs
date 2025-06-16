using Microsoft.EntityFrameworkCore;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<Merchant> Merchants { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Merchant>(e =>
        {
            e.Property(m => m.Name).IsRequired().HasMaxLength(100);
            e.Property(m => m.Email).IsRequired();
            e.Property(m => m.Category).HasConversion<string>().IsRequired();
            e.Property(m => m.CreatedAt).HasDefaultValueSql("GetUtcDate()");
        });
    }
}