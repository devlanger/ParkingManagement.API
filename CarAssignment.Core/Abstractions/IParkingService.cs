using CarAssignment.Core.Data;
using CarAssignment.Core.Data.Enums;

namespace CarAssignment.Core.Abstractions;

public interface IParkingService
{ 
    Task<Car> AllocateCarAsync(string vehicleRegistration, VehicleType vehicleType, CancellationToken cancellationToken);
    Task<Car> DeallocateCarAsync(string vehicleRegistration, CancellationToken cancellationToken);
    TimeSpan GetParkingTime(Car car);
}