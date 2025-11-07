using System.Data;
using CarAssignment.Application.CQRS.Models;
using CarAssignment.Core.Abstractions;
using CarAssignment.Core.Exceptions;
using CarAssignment.Infrastructure;
using CarAssignment.Infrastructure.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;

public class DeallocateVehicleCommandHandler(
    IParkingService parkingService, 
    IPaymentService paymentService, 
    IValidator<DeallocateVehicleCommand> validator,
    ICarRepository carRepository,
    ParkingDbContext dbContext)
    : IRequestHandler<DeallocateVehicleCommand, DeallocateVehicleCommandResponse>
{
    public async Task<DeallocateVehicleCommandResponse> Handle(DeallocateVehicleCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var c = await carRepository.GetParkedCarByRegistrationAsync(request.VehicleRegistration);
        if(c is null)
            throw new NotFoundException($"Vehicle with registration: {request.VehicleRegistration} not found.");
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

        try
        {
            var car = await parkingService.DeallocateCarAsync(request.VehicleRegistration, cancellationToken);
            var chargeAmount = await paymentService.ChargeCar(car);
            await transaction.CommitAsync(cancellationToken);

            return new DeallocateVehicleCommandResponse(car.RegistrationNumber,
                chargeAmount,
                car.ParkingEnterTime,
                car.ParkingExitTime.GetValueOrDefault());
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}