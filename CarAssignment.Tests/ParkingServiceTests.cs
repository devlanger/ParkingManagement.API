using CarAssignment.Application.Services;
using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Configuration;
using CarAssignment.Core.Data;
using CarAssignment.Core.Data.Enums;
using CarAssignment.Core.Exceptions;
using CarAssignment.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;

namespace CarAssignment.Tests;

public class ParkingServiceTests
{
    private readonly Mock<IOptions<ParkingConfiguration>> _parkingConfigurationMock = new();
    private readonly Mock<IParkingSlotRepository> _parkingSlotRepositoryMock = new();
    private readonly Mock<ICarRepository> _carRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private readonly IParkingService _sut;

    public ParkingServiceTests()
    {
        _unitOfWorkMock.Setup(x => x.CarRepository).Returns(_carRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.ParkingSlotRepository).Returns(_parkingSlotRepositoryMock.Object);
        
        _sut = new ParkingService(_unitOfWorkMock.Object, _parkingConfigurationMock.Object);
    }

    [Fact]
    public async Task ThrowException_WhenAddingExistingCar()
    {
        //Arrange
        var car = new Car
        {
            RegistrationNumber = "test",
            VehicleType = VehicleType.LARGE_CAR,
            ParkingEnterTime = DateTimeOffset.UtcNow
        };

        _parkingSlotRepositoryMock.Setup(x =>
                x.GetParkingSlotAsync(It.IsAny<string>()))
            .ReturnsAsync(new ParkingSlot()
            {
                CarId = car.Id
            });

        _parkingSlotRepositoryMock.Setup(x => x.GetFreeParkingSlotAsync()).ReturnsAsync(new ParkingSlot());
        
        //Assert
        await Assert.ThrowsAsync<ConflictException>(() =>
            _sut.AllocateCarAsync("test", VehicleType.LARGE_CAR, CancellationToken.None));
    }

    [Fact]
    public async Task AdditionalCharge_IsTrue_WhenAllocatingCarWithAvailableSlotAmountLessThan()
    {
        //Arrange
        _parkingConfigurationMock
            .Setup(x => x.Value)
            .Returns(new ParkingConfiguration()
            {
                AdditionalChargeParkingSlotsAmount = 5
            });
        
        for (var i = 0; i < 6; i++)
        {
            _parkingSlotRepositoryMock.Setup(x =>
                    x.GetParkingSlotAsync($"reg-{i}"))
                .ReturnsAsync(
                    new ParkingSlot()
                    {
                        Id = i,
                        CarId = i
                    });

            _parkingSlotRepositoryMock.Setup(x =>
                x.GetFreeParkingSlotAsync()).ReturnsAsync(new ParkingSlot());
        }

        //Act
        var allocatedCar =
            await _sut.AllocateCarAsync("test-123", VehicleType.LARGE_CAR, CancellationToken.None);

        //Assert
        Assert.True(allocatedCar.ChargeAdditional);
    }

    [Fact]
    public async Task ThrowException_WhenRemovingNonExistingCar()
    {
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _sut.DeallocateCarAsync("non-existing-reg", CancellationToken.None));
    }

    [Fact]
    public async Task ThrowException_WhenNoAvailableSpace()
    {
        //Arrange
        _parkingSlotRepositoryMock.Setup(x => 
            x.GetFreeParkingSlotAsync())
            .ReturnsAsync(() => null);
        
        //Assert
        await Assert.ThrowsAsync<NotAvailableSpaceException>(() =>
            _sut.AllocateCarAsync("test-9999", It.IsAny<VehicleType>(), CancellationToken.None));
    }
}