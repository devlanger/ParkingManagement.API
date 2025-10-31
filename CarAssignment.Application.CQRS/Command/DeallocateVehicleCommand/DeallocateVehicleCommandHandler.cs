using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Data;
using MediatR;

namespace CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;

public class DeallocateVehicleCommandHandler(IParkingService parkingService, IPaymentService paymentService) : IRequestHandler<DeallocateVehicleCommand>
{
    public async Task Handle(DeallocateVehicleCommand request, CancellationToken cancellationToken)
    {
        Car c = null;
        if(c == null)
            throw new NullReferenceException($"Couldn't non existing deallocate vehicle {request.VehicleRegistration}");
        
        paymentService.Charge(c, parkingService.GetAmountForParking(c));
        
        await parkingService.DeallocateCar(request.VehicleRegistration);
    }
}