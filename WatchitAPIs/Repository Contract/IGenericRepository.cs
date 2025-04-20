namespace WatchitAPIs.Repository_Contract;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(object id);
    Task AddAsync(T entity);
    Task<bool> DeleteAsync(object id);

    Task UpdateAsync(T entity);
    Task SaveAsync();

    Task<bool> UpdateEntityAsync(object id, T newEntity);
}
