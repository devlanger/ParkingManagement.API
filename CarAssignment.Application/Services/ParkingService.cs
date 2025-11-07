using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Configuration;
using CarAssignment.Core.Data;
using CarAssignment.Core.Data.Enums;
using CarAssignment.Core.Exceptions;
using CarAssignment.Core.Factories;
using CarAssignment.Infrastructure;
using Microsoft.Extensions.Options;

namespace CarAssignment.Application.Services;

public class ParkingService(
    ICarRepository carRepository,
    IParkingSlotRepository parkingSlotRepository,
    IOptions<ParkingConfiguration> parkingConfiguration) : IParkingService
{
    public async Task<Car> AllocateCarAsync(string vehicleReg, VehicleType vehicleType, CancellationToken cancellationToken)
    {
        var freeParkingSlot = await parkingSlotRepository.GetFreeParkingSlotAsync();
        if (freeParkingSlot is null)
            throw new NotAvailableSpaceException();

        var parkingSlotForCar = await parkingSlotRepository.GetParkingSlotAsync(vehicleReg);
        if (parkingSlotForCar is not null)
            throw new ConflictException($"There is already parked car with vehicle registration: {vehicleReg}");

        var car = CarFactory.CreateCar(vehicleType);
        car.RegistrationNumber = vehicleReg;
        car.VehicleType = vehicleType;
        car.ParkingEnterTime = DateTimeOffset.UtcNow;
        car.ChargeAdditional = parkingSlotRepository.GetAvailableCapacity() < parkingConfiguration.Value.AdditionalChargeParkingSlotsAmount;

        await carRepository.AddAsync(car);  

        freeParkingSlot.Car = car;
        freeParkingSlot.CarId = car.Id;

        await parkingSlotRepository.UpdateAsync(freeParkingSlot);
        
        return car;
    }
    
    public async Task<Car> DeallocateCarAsync(string vehicleRegistration, CancellationToken cancellationToken)
    {
        var occupiedParkingSlot = await parkingSlotRepository.GetParkingSlotAsync(vehicleRegistration);

        if (occupiedParkingSlot?.Car is null)
            throw new NotFoundException($"Parking slot for a car: {vehicleRegistration} not found.");

        var parkedCar = occupiedParkingSlot.Car;
        occupiedParkingSlot.Car.ParkingExitTime = DateTimeOffset.UtcNow;
        await carRepository.UpdateAsync(occupiedParkingSlot.Car);
    
        occupiedParkingSlot.CarId = null;
        await parkingSlotRepository.UpdateAsync(occupiedParkingSlot);
        
        return parkedCar;
    }

    public TimeSpan GetParkingTime(Car car) => DateTimeOffset.UtcNow - car.ParkingEnterTime;
}