namespace CarAssignment.Core;

public class InvalidCarTypeException(VehicleType vehicleType) : Exception
{
    public override string Message => $"Couldn't create instance of vehicle: {vehicleType}";
}