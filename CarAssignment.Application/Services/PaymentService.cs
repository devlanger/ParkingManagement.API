using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Data;
using CarAssignment.Core.Factories;
using CarAssignment.Infrastructure;

namespace CarAssignment.Application.Services;

public class PaymentService(IParkingService parkingService, IRepository<Car> carRepository) : IPaymentService
{
    public async Task<double> ChargeCar(Car carToCharge)
    {
        var chargeAmount = GetAmountForParking(carToCharge);
        carToCharge.ChargeAmount = chargeAmount;
        await carRepository.UpdateAsync(carToCharge);
        return chargeAmount;
    }
    
    private double GetAmountForParking(Car car)
    {
        var totalMinutes = parkingService.GetParkingTime(car).TotalMinutes;
        var carChargePricePerMinute = CarFactory.CreateCarChargePricePerMinute(car.VehicleType) * totalMinutes;
        
        //Charge additional dollar every 5 minutes
        carChargePricePerMinute += (totalMinutes / 5) * 1.0;
        
        return carChargePricePerMinute;
    }
}