using CarAssignment.Core.Data;
using CarAssignment.Core.Data.Enums;

namespace CarAssignment.Core.Abstractions;

public interface IParkingService
{ 
    Task<Car> AllocateCarAsync(string vehicleRegistration, VehicleType vehicleType);
    Task<Car> DeallocateCarAsync(string vehicleRegistration);
    int GetAvailableCapacity();
    int GetOccupiedCapacity();
    TimeSpan GetParkingTime(Car car);
    Task<Car?> GetParkedCarByRegistrationAsync(string requestVehicleRegistration);
    Task<ParkingSlot?> GetParkingSlotByCarIdAsync(int carId);
    Task<ParkingSlot?> GetFreeParkingSlotAsync();
}