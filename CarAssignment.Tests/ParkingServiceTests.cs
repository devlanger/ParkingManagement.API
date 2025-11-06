using CarAssignment.Application.Services;
using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Configuration;
using CarAssignment.Core.Data;
using CarAssignment.Core.Data.Enums;
using CarAssignment.Core.Exceptions;
using CarAssignment.Infrastructure.Database;
using Microsoft.Extensions.Options;
using Moq;

namespace CarAssignment.Tests;

public class ParkingServiceTests : TestBase
{
    private readonly IOptions<ParkingConfiguration> _parkingConfiguration;

    private readonly Mock<ParkingDbContext> _mockParkingDbContext;
    private readonly IParkingService _parkingService;
    
    public ParkingServiceTests()
    {
        var carRepositoryMock = CreateRepositoryMock<Car>();
        var parkingSlotRepositoryMock = CreateRepositoryMock<ParkingSlot>();
        _mockParkingDbContext = new Mock<ParkingDbContext>();

        _parkingConfiguration = Options.Create(new ParkingConfiguration()
        {
            ParkingSlotCount = 5,
            AdditionalChargeParkingSlotsAmount = 5
        });
        
        _parkingService = new ParkingService(carRepositoryMock.Object,
            parkingSlotRepositoryMock.Object,
            _parkingConfiguration,
            _mockParkingDbContext.Object);
        
        SeedParkingSlots();
    }

    private void SeedParkingSlots()
    {
        var parkingSlots = new List<ParkingSlot>();
        for (var i = 0; i < _parkingConfiguration.Value.ParkingSlotCount; i++)
        {
            parkingSlots.Add(new ParkingSlot());
        }
        
        SeedEntities(parkingSlots.ToArray());
    }
    
    [Fact]
    public async Task ThrowException_WhenAddingExistingCar()
    {
        var car = new Car
        {
            RegistrationNumber = "test",
            VehicleType = VehicleType.LARGE_CAR,
            ParkingEnterTime = DateTimeOffset.UtcNow
        };
        
        SeedEntities(car);
        SeedEntities(new ParkingSlot()
        {
            CarId = car.Id
        });
        
        await Assert.ThrowsAsync<ConflictException>(() => 
            _parkingService.AllocateCarAsync("test", VehicleType.LARGE_CAR, CancellationToken.None));
    }
    
    [Fact]
    public async Task AdditionalCharge_IsTrue_WhenAllocatingCarWithAvailableSlotAmountLessThan()
    {
        //Arrange
        var availableSlotsToTakeCharge = 6;
        
        ClearEntities<Car>();

        var entities = new List<Car>();
        for (var i = 0; i < availableSlotsToTakeCharge; i++)
        {
            entities.Add(new Car
            {
                RegistrationNumber = $"test-{i}",
                VehicleType = VehicleType.LARGE_CAR,
                ParkingEnterTime = DateTimeOffset.UtcNow
            });
        }
        
        SeedEntities(entities.ToArray());
        
        var allocatedCar = await _parkingService.AllocateCarAsync("test-123", VehicleType.LARGE_CAR, CancellationToken.None);
        
        Assert.True(allocatedCar.ChargeAdditional);
    }
    
    [Fact]
    public async Task ThrowException_WhenRemovingNonExistingCar()
    {
        ClearEntities<Car>();

        SeedEntities(new Car
        {
            RegistrationNumber = "test",
            VehicleType = VehicleType.LARGE_CAR,
            ParkingEnterTime = DateTimeOffset.UtcNow
        });
        
        await Assert.ThrowsAsync<NotFoundException>(() => 
            _parkingService.DeallocateCarAsync("non-existing-test", CancellationToken.None));
    }
    
    [Fact]
    public async Task ThrowException_WhenNoAvailableSpace()
    {
        ClearEntities<Car>();

        var entities = new List<Car>();
        for (var i = 0; i < _parkingConfiguration.Value.ParkingSlotCount; i++)
        {
            entities.Add(new Car
            {
                RegistrationNumber = $"test-{i}",
                VehicleType = VehicleType.LARGE_CAR,
                ParkingEnterTime = DateTimeOffset.UtcNow
            });
        }
        
        SeedEntities(entities.ToArray());
        
        await Assert.ThrowsAsync<NotAvailableSpaceException>(() => 
            _parkingService.AllocateCarAsync("test-9999", It.IsAny<VehicleType>(), CancellationToken.None));
    }

    public override void Dispose()
    {
        ClearEntities<ParkingSlot>();
        ClearEntities<Car>();
        base.Dispose();
    }
}