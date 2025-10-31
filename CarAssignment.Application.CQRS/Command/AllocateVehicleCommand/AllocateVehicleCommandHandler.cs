using CarAssignment.Core.Abstractions;
using MediatR;

namespace CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;

public class AllocateVehicleCommandHandler(IParkingService parkingService) : IRequestHandler<AllocateVehicleCommand>
{
    public Task Handle(AllocateVehicleCommand request, CancellationToken cancellationToken)
    {
        parkingService.AllocateCar(request.VehicleRegistration, request.VehicleType);
        return Task.CompletedTask;
    }
}