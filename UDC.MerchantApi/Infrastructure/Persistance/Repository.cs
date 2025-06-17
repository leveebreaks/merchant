using Microsoft.EntityFrameworkCore;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Infrastructure.Persistance;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext Db;

    public Repository(AppDbContext db)
    {
        Db = db;
    }
    
    public async Task<T?> GetByIdAsync(int id)
    {
        return await Db.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Db.Set<T>().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await Db.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        if (entity is ISoftDeletable softDeletable)
        {
            softDeletable.IsDeleted = true;
            return;
        }
        
        Db.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await Db.SaveChangesAsync();
    }
}