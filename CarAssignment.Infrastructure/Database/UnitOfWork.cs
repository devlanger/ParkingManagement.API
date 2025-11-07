using CarAssignment.Core.Abstractions;

namespace CarAssignment.Infrastructure.Database;

public class UnitOfWork(
    ParkingDbContext dbContext,
    IParkingSlotRepository parkingSlotRepository,
    ICarRepository carRepository) : IUnitOfWork
{
    public IParkingSlotRepository ParkingSlotRepository { get; } = parkingSlotRepository;
    public ICarRepository CarRepository { get; } = carRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
    }
    
    public void Dispose()
    {
        dbContext.Dispose();
    }
}