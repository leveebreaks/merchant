using System.Reflection;
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
        
        foreach (EntityEntry<ISoftDeletable> entry in ChangeTracker.Entries<ISoftDeletable>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
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
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(AppDbContext)
                    .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, [modelBuilder]);
            }
        }
    }
    
    private static void SetSoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : class, ISoftDeletable
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }
}