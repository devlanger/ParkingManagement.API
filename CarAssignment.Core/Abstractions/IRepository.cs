using Microsoft.EntityFrameworkCore;

namespace CarAssignment.Infrastructure;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    void Delete(T entity);
    Task<T?> GetByIdAsync(string id);
    IQueryable<T> Query();
}