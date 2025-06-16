using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<Merchant> Merchants { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        foreach (EntityEntry<IAuditable> entry in ChangeTracker.Entries<IAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = utcNow;
                    break;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Merchant>(e =>
        {
            e.Property(m => m.Name).IsRequired().HasMaxLength(100);
            e.Property(m => m.Email).IsRequired();
            e.Property(m => m.Category).HasConversion<string>().IsRequired();
        });
    }
}