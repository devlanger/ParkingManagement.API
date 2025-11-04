using CarAssignment.Application.Configuration;
using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Data;
using CarAssignment.Core.Data.Enums;
using CarAssignment.Core.Exceptions;
using CarAssignment.Core.Factories;
using CarAssignment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarAssignment.Application.Services;

public class ParkingService(
    IRepository<Car> carRepository,
    IRepository<ParkingSlot> parkingSlotRepository) : IParkingService
{
    public async Task<Car> AllocateCarAsync(string vehicleReg, VehicleType vehicleType)
    {
        var parkingSlotForCar = await GetParkingSlotAsync(vehicleReg);

        var freeParkingSlot = await GetFreeParkingSlotAsync();

        if (freeParkingSlot is null)
            throw new NotAvailableSpaceException();

        if (parkingSlotForCar is not null)
            throw new ConflictException($"There is already parked car with vehicle registration: {vehicleReg}");

        var car = CarFactory.CreateCar(vehicleType);
        car.RegistrationNumber = vehicleReg;
        car.VehicleType = vehicleType;
        car.ParkingEnterTime = DateTimeOffset.UtcNow;

        await carRepository.AddAsync(car);  

        freeParkingSlot.Car = car;
        freeParkingSlot.CarId = car.Id;

        await parkingSlotRepository.UpdateAsync(freeParkingSlot);
        return car;
    }
    
    public async Task<Car> DeallocateCarAsync(string vehicleRegistration)
    {
        var occupiedParkingSlot = await GetParkingSlotAsync(vehicleRegistration);

        if (occupiedParkingSlot?.Car is null)
            throw new NotFoundException($"Parking slot for a car: {vehicleRegistration} not found.");

        var parkedCar = occupiedParkingSlot.Car;
        occupiedParkingSlot.Car.ParkingExitTime = DateTimeOffset.UtcNow;
        await carRepository.UpdateAsync(occupiedParkingSlot.Car);
        
        occupiedParkingSlot.CarId = null;
        await parkingSlotRepository.UpdateAsync(occupiedParkingSlot);
        return parkedCar;
    }
    
    public int GetAvailableCapacity() => parkingSlotRepository.Query().AsNoTracking().Count(x => x.CarId == null);
    public int GetOccupiedCapacity() => parkingSlotRepository.Query().AsNoTracking().Count(x => x.CarId != null);

    public TimeSpan GetParkingTime(Car car) => DateTimeOffset.UtcNow - car.ParkingEnterTime;

    private async Task<ParkingSlot?> GetParkingSlotAsync(string registrationNumber) => await parkingSlotRepository
        .Query()
        .Include(c => c.Car)
        .FirstOrDefaultAsync(c => c.Car != null && c.Car.RegistrationNumber == registrationNumber);
    
    public async Task<Car?> GetParkedCarByRegistrationAsync(string vehicleRegistration) =>
        await carRepository
            .Query()
            .FirstOrDefaultAsync(c => c.RegistrationNumber == vehicleRegistration && c.ParkingExitTime == null);

    public async Task<ParkingSlot?> GetParkingSlotByCarIdAsync(int carId) => 
        await parkingSlotRepository
            .Query()
            .FirstOrDefaultAsync(c => c.Car != null && c.CarId == carId);

    public async Task<ParkingSlot?> GetFreeParkingSlotAsync() =>
        await parkingSlotRepository
            .Query()
            .FirstOrDefaultAsync(c => c.Car == null);
}