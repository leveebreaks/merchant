using Microsoft.EntityFrameworkCore;
using UDC.MerchantApi.Domain;
using UDC.MerchantApi.Infrastructure.Persistance;

namespace UDC.MerchantApi.Features.Merchants;

public class MerchantRepository : Repository<Merchant>, IMerchantRepository
{
    public MerchantRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<List<Merchant>> GetFilteredAsync(Category? category, string? name)
    {
        var query = Db.Merchants.AsNoTracking();
        if (category is not null)
        {
            query = query.Where(x => x.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(x => x.Name == name);
        }
        
        return await query.ToListAsync();
    }
}