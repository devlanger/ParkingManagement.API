using CarAssignment.Core.Data;

namespace CarAssignment.Infrastructure.Database;

public class ParkingSeedData
{
    public static async Task SeedParkingSlotData(ParkingDbContext parkingDbContext)
    {
        if (parkingDbContext.ParkingSlots.Count() != 0)
            return;

        var parkingSlots = new List<ParkingSlot>();
        for (var i = 0; i < 10; i++)
        {
            parkingSlots.Add(new ParkingSlot());
        }

        await parkingDbContext.ParkingSlots.AddRangeAsync(parkingSlots);
        await parkingDbContext.SaveChangesAsync();
    } 
}