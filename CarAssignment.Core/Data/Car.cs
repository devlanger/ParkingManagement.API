namespace CarAssignment.Core.Data;

public abstract class Car : Vehicle
{
    public string RegistrationNumber { get; set; }
    public DateTimeOffset ParkingEnterTime { get; set; }
    public virtual double PricePerMinute { get; }
}