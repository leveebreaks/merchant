namespace UDC.MerchantApi.Domain;

public interface IMerchantRepository : IRepository<Merchant>
{
    Task<List<Merchant>> GetFilteredAsync(Category? category, string? name);
}