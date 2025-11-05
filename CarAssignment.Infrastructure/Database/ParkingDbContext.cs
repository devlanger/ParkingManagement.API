using CarAssignment.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAssignment.Infrastructure.Database;

public class ParkingDbContext(DbContextOptions<ParkingDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<ParkingSlot> ParkingSlots { get; set; }
}