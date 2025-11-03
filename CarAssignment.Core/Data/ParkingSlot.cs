namespace CarAssignment.Core.Data;

public class ParkingSlot : BaseEntity
{
    public int? CarId { get; set; }
    public Car? Car { get; set; }
}