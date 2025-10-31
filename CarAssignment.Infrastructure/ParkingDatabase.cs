using CarAssignment.Core.Data;

namespace CarAssignment.Infrastructure;

public class ParkingDatabaseContext : IRepository<Car>
{
    private Dictionary<string, Car> Cars { get; set; } = new Dictionary<string, Car>();
    
    public void Add(Car entity)
    {
        Cars.Add(entity.RegistrationNumber, entity);
    }

    public void Update(Car entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Car entity)
    {
        Cars.Remove(entity.RegistrationNumber);
    }

    public Task<Car?> GetByIdAsync(string id)
    {
        return Task.FromResult(Cars.GetValueOrDefault(id));
    }

    public IQueryable<Car> GetAll()
    {
        return Cars.Values.AsQueryable();
    }
}