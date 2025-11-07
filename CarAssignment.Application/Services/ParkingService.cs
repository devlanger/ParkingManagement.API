using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Configuration;
using CarAssignment.Core.Data;
using CarAssignment.Core.Data.Enums;
using CarAssignment.Core.Exceptions;
using CarAssignment.Core.Factories;
using Microsoft.Extensions.Options;

namespace CarAssignment.Application.Services;

public class ParkingService(
    IUnitOfWork unitOfWork,
    IOptions<ParkingConfiguration> parkingConfiguration) : IParkingService
{
    public async Task<Car> AllocateCarAsync(string vehicleReg, VehicleType vehicleType, CancellationToken cancellationToken)
    {
        var freeParkingSlot = await unitOfWork.ParkingSlotRepository.GetFreeParkingSlotAsync();
        if (freeParkingSlot is null)
            throw new NotAvailableSpaceException();

        var parkingSlotForCar = await unitOfWork.ParkingSlotRepository.GetParkingSlotAsync(vehicleReg);
        if (parkingSlotForCar is not null)
            throw new ConflictException($"There is already parked car with vehicle registration: {vehicleReg}");

        var car = CarFactory.CreateCar(vehicleType);
        car.RegistrationNumber = vehicleReg;
        car.VehicleType = vehicleType;
        car.ParkingEnterTime = DateTimeOffset.UtcNow;
        car.ChargeAdditional = unitOfWork.ParkingSlotRepository.GetAvailableCapacity() < parkingConfiguration.Value.AdditionalChargeParkingSlotsAmount;

        await unitOfWork.CarRepository.AddAsync(car);  

        freeParkingSlot.Car = car;
        freeParkingSlot.CarId = car.Id;
        unitOfWork.ParkingSlotRepository.Update(freeParkingSlot);

        await unitOfWork.SaveChangesAsync(cancellationToken);        
        
        return car;
    }
    
    public async Task<Car> DeallocateCarAsync(string vehicleRegistration, CancellationToken cancellationToken)
    {
        var occupiedParkingSlot = await unitOfWork.ParkingSlotRepository.GetParkingSlotAsync(vehicleRegistration);

        if (occupiedParkingSlot?.Car is null)
            throw new NotFoundException($"Parking slot for a car: {vehicleRegistration} not found.");

        var parkedCar = occupiedParkingSlot.Car;
        occupiedParkingSlot.Car.ParkingExitTime = DateTimeOffset.UtcNow;
        unitOfWork.CarRepository.Update(occupiedParkingSlot.Car);
    
        occupiedParkingSlot.CarId = null;
        unitOfWork.ParkingSlotRepository.Update(occupiedParkingSlot);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return parkedCar;
    }

    public TimeSpan GetParkingTime(Car car) => DateTimeOffset.UtcNow - car.ParkingEnterTime;
}