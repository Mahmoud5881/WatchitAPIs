using Microsoft.EntityFrameworkCore;
using WatchitAPIs.EFCore;
using WatchitAPIs.Repository_Contract;

namespace WatchitAPIs.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext context;
    private readonly DbSet<T> dbSet;

    public GenericRepository(AppDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

    public async Task<T> GetByIdAsync(object id) => await dbSet.FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        await SaveAsync();
    }


    public async Task<bool> UpdateEntityAsync(object id,T newEntity)
    {
        var oldEntity = await dbSet.FindAsync(id);
        if (oldEntity != null)
        {
            context.Entry(oldEntity).CurrentValues.SetValues(newEntity);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(object id)
    {
        var entity = await dbSet.FindAsync(id);
        if(entity != null)
            dbSet.Remove(entity);
        await SaveAsync();
        if (await dbSet.FindAsync(id) != null)
            return false;
        return true;
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}