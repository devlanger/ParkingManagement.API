using CarAssignment.Application.Configuration;
using CarAssignment.Core;
using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Data;
using CarAssignment.Infrastructure;
using Microsoft.Extensions.Options;

namespace CarAssignment.Application.Services;

public class ParkingService(IRepository<Car> carRepository, IOptions<ParkingConfiguration> parkingConfiguration) : IParkingService
{
    public Task<Car> AllocateCar(string vehicleReg, VehicleType vehicleType)
    {
        var car = CarFactory.CreateCar(vehicleType);
        car.RegistrationNumber = vehicleReg;
        car.ParkingEnterTime = DateTimeOffset.UtcNow;
        carRepository.Add(car);
        return Task.FromResult(car);
    }

    public async Task DeallocateCar(string vehicleRegistration)
    {
        var car = await carRepository.GetByIdAsync(vehicleRegistration);

        if (car is null)
            throw new Exception($"Car with id {vehicleRegistration} not found.");
        
        carRepository.Delete(car);
    }

    public int GetAvailableCapacity() => parkingConfiguration.Value.ParkingSlotCount - carRepository.GetAll().Count();
    public int GetOccupiedCapacity() => carRepository.GetAll().Count();
    public double GetAmountForParking(Car car) => car.PricePerMinute * GetParkingTime(car).Minutes;
    public TimeSpan GetParkingTime(Car car) => DateTimeOffset.UtcNow - car.ParkingEnterTime;
}