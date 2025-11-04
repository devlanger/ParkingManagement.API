using CarAssignment.Core.Configuration;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;

public class DeallocateVehicleCommandValidator : AbstractValidator<DeallocateVehicleCommand>
{
    public DeallocateVehicleCommandValidator(IOptions<ParkingConfiguration> options)
    {
        RuleFor(x => x.VehicleRegistration)
            .NotEmpty()
            .Length(options.Value.VehicleRegistrationNumberLength);
    }
}