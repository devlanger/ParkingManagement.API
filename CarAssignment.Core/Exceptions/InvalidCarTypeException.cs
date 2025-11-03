using CarAssignment.Core.Data.Enums;

namespace CarAssignment.Core.Exceptions;

public class InvalidCarTypeException(VehicleType vehicleType) : Exception
{
    public override string Message => $"Couldn't create instance of vehicle: {vehicleType}";
}