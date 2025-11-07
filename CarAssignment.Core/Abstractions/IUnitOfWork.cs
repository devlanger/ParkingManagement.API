using CarAssignment.Infrastructure;

namespace CarAssignment.Core.Abstractions;

public interface IUnitOfWork : IDisposable
{
    ICarRepository CarRepository { get; }   
    IParkingSlotRepository ParkingSlotRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}