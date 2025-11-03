using CarAssignment.Application.CQRS.Models;
using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Exceptions;
using MediatR;

namespace CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;

public class DeallocateVehicleCommandHandler(IParkingService parkingService, IPaymentService paymentService)
    : IRequestHandler<DeallocateVehicleCommand, DeallocateVehicleCommandResponse>
{
    public async Task<DeallocateVehicleCommandResponse> Handle(DeallocateVehicleCommand request, CancellationToken cancellationToken)
    {
        var c = await parkingService.GetParkedCarByRegistrationAsync(request.VehicleRegistration);
        if(c is null)
            throw new NotFoundException($"Vehicle with registration: {request.VehicleRegistration} not found.");

        var car = await parkingService.DeallocateCarAsync(request.VehicleRegistration);
        var chargeAmount = await paymentService.ChargeCar(car);
        
        return new DeallocateVehicleCommandResponse(car.RegistrationNumber,
            chargeAmount,
            car.ParkingEnterTime,
            car.ParkingExitTime.GetValueOrDefault());
    }
}