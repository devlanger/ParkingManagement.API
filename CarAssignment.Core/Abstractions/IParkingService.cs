using CarAssignment.Core.Data;

namespace CarAssignment.Core.Abstractions;

public interface IParkingService
{ 
    Task<Car> AllocateCar(string vehicleRegistration, VehicleType vehicleType);
    Task DeallocateCar(string vehicleRegistration);
    int GetAvailableCapacity();
    int GetOccupiedCapacity();
    double GetAmountForParking(Car car);
    TimeSpan GetParkingTime(Car car);
}