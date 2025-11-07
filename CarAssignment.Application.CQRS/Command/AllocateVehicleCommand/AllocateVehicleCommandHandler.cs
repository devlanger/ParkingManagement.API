using CarAssignment.Core.Abstractions;
using CarAssignment.Infrastructure;
using FluentValidation;
using MediatR;

namespace CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;

public class AllocateVehicleCommandHandler(
    IParkingService parkingService, 
    IParkingSlotRepository parkingSlotRepository, 
    IValidator<AllocateVehicleCommand> validator)
    : IRequestHandler<AllocateVehicleCommand, AllocateVehicleCommandResponse>
{
    public async Task<AllocateVehicleCommandResponse> Handle(AllocateVehicleCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var car = await parkingService.AllocateCarAsync(request.VehicleRegistration, request.VehicleType, cancellationToken);
        var allocatedParkingSlot = await parkingSlotRepository.GetParkingSlotByCarIdAsync(car.Id);
        
        if(allocatedParkingSlot == null)
            throw new Exception($"Not found parking slot for car: {car.Id}");
        
        return new AllocateVehicleCommandResponse(car.RegistrationNumber,
            allocatedParkingSlot.Id,
            car.ParkingEnterTime.UtcDateTime);
    }
}