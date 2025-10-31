namespace CarAssignment.Infrastructure;

public interface IRepository<T>
{
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task<T?> GetByIdAsync(string id);
    public IQueryable<T> GetAll();
}