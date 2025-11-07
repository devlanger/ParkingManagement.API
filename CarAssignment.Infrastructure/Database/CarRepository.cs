using CarAssignment.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAssignment.Infrastructure.Database;

public class CarRepository(ParkingDbContext dbContext) : ICarRepository
{
    public async Task AddAsync(Car entity)
    {
        await dbContext.Set<Car>().AddAsync(entity);
    }

    public void Update(Car entity)
    {
        dbContext.Set<Car>().Attach(entity);
        dbContext.Set<Car>().Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Car entity)
    {
        dbContext.Set<Car>().Remove(entity);
        dbContext.SaveChanges();
    }
    
    public async Task<Car?> GetByIdAsync(string id) => await dbContext.Set<Car>().FindAsync(id);
    
    public async Task<Car?> GetParkedCarByRegistrationAsync(string vehicleRegistration) =>
        await dbContext.Cars.
            FirstOrDefaultAsync(c => c.RegistrationNumber == vehicleRegistration 
                                     && c.ParkingExitTime == null);
}