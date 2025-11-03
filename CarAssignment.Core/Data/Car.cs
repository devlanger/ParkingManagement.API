using CarAssignment.Core.Data.Enums;

namespace CarAssignment.Core.Data;

public class Car : BaseEntity
{
    public string RegistrationNumber { get; set; }
    public VehicleType VehicleType { get; set; }
    public DateTimeOffset ParkingEnterTime { get; set; }
    public DateTimeOffset? ParkingExitTime { get; set; }
    public double? ChargeAmount { get; set; }
}