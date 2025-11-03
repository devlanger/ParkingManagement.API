
using Microsoft.EntityFrameworkCore;

namespace CarAssignment.Infrastructure.Database;

public class GenericRepository<T>(ParkingDbContext dbContext) : IRepository<T> where T : class
{
    public async Task AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Attach(entity);
        dbContext.Set<T>().Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        dbContext.SaveChanges();
    }
    
    public async Task<T?> GetByIdAsync(string id) => await dbContext.Set<T>().FindAsync(id);

    public IQueryable<T> Query()
    {
        return dbContext.Set<T>().AsQueryable();
    }
}