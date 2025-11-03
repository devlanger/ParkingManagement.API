using CarAssignment.Core.Data;
using CarAssignment.Core.Data.Enums;
using CarAssignment.Core.Exceptions;

namespace CarAssignment.Core.Factories;

public sealed class CarFactory
{
    public static Car CreateCar(VehicleType vehicleType)
    {
        var car = new Car();
        return vehicleType switch
        {
            VehicleType.SMALL_CAR => car,
            VehicleType.MEDIUM_CAR => car,
            VehicleType.LARGE_CAR => car,
            _ => throw new InvalidCarTypeException(vehicleType: vehicleType)
        };
    }
    
    public static double CreateCarChargePricePerMinute(VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.SMALL_CAR => 0.1,
            VehicleType.MEDIUM_CAR => 0.2,
            VehicleType.LARGE_CAR => 0.4,
            _ => throw new InvalidCarTypeException(vehicleType: vehicleType)
        };
    }
}