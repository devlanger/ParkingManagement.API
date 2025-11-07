using CarAssignment.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAssignment.Infrastructure.Database;

public class ParkingSlotRepository(ParkingDbContext dbContext) : IParkingSlotRepository
{
    public async Task AddAsync(ParkingSlot entity)
    {
        await dbContext.Set<ParkingSlot>().AddAsync(entity);
    }

    public void Update(ParkingSlot entity)
    {
        dbContext.Set<ParkingSlot>().Attach(entity);
        dbContext.Set<ParkingSlot>().Entry(entity).State = EntityState.Modified;
    }

    public void Delete(ParkingSlot entity)
    {
        dbContext.Set<ParkingSlot>().Remove(entity);
        dbContext.SaveChanges();
    }
    
    public async Task<ParkingSlot?> GetByIdAsync(string id) => await dbContext.Set<ParkingSlot>().FindAsync(id);
    
    public int GetAvailableCapacity() => dbContext.ParkingSlots.AsNoTracking().Count(x => x.CarId == null);
    public int GetOccupiedCapacity() => dbContext.ParkingSlots.AsNoTracking().Count(x => x.CarId != null);
    
    
    public async Task<ParkingSlot?> GetParkingSlotAsync(string registrationNumber) => await dbContext.ParkingSlots
        .Include(c => c.Car)
        .FirstOrDefaultAsync(c => c.Car != null && c.Car.RegistrationNumber == registrationNumber);

    public async Task<ParkingSlot?> GetParkingSlotByCarIdAsync(int carId) => 
        await dbContext.ParkingSlots.FirstOrDefaultAsync(c => c.Car != null && c.CarId == carId);

    public async Task<ParkingSlot?> GetFreeParkingSlotAsync() =>
        await dbContext.ParkingSlots.FirstOrDefaultAsync(c => c.Car == null);
}