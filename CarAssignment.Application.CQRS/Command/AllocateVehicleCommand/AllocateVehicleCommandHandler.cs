using CarAssignment.Core.Abstractions;
using MediatR;

namespace CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;

public class AllocateVehicleCommandHandler(IParkingService parkingService)
    : IRequestHandler<AllocateVehicleCommand, AllocateVehicleCommandResponse>
{
    public async Task<AllocateVehicleCommandResponse> Handle(AllocateVehicleCommand request, CancellationToken cancellationToken)
    {
        var car = await parkingService.AllocateCarAsync(request.VehicleRegistration, request.VehicleType);
        var allocatedParkingSlot = await parkingService.GetParkingSlotByCarIdAsync(car.Id);
        
        if(allocatedParkingSlot == null)
            throw new Exception($"Not found parking slot for car: {car.Id}");
        
        return new AllocateVehicleCommandResponse(car.RegistrationNumber,
            allocatedParkingSlot.Id,
            car.ParkingEnterTime.UtcDateTime);
    }
}