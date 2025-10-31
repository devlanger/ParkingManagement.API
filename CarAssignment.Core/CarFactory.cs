using CarAssignment.Core.Data;

namespace CarAssignment.Core;

public sealed class CarFactory
{
    public static Car CreateCar(VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.SMALL_CAR => new SmallCar(),
            VehicleType.MEDIUM_CAR => new MediumCar(),
            VehicleType.LARGE_CAR => new LargeCar(),
            _ => throw new InvalidCarTypeException(vehicleType: vehicleType)
        };
    }
}