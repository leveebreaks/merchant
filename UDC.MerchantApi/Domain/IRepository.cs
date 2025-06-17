namespace UDC.MerchantApi.Domain;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}