using CarAssignment.Core.Data;

namespace CarAssignment.Infrastructure;

public interface IParkingSlotRepository
{
    Task AddAsync(ParkingSlot entity);
    void Update(ParkingSlot entity);
    void Delete(ParkingSlot entity);
    Task<ParkingSlot?> GetByIdAsync(string id);
    
    int GetAvailableCapacity();
    int GetOccupiedCapacity();

    Task<ParkingSlot?> GetParkingSlotAsync(string registrationNumber);
    Task<ParkingSlot?> GetParkingSlotByCarIdAsync(int carId);
    Task<ParkingSlot?> GetFreeParkingSlotAsync();
}