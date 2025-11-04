using CarAssignment.Core.Configuration;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;

public class AllocateVehicleCommandValidator : AbstractValidator<AllocateVehicleCommand>
{
    public AllocateVehicleCommandValidator(IOptions<ParkingConfiguration> options)
    {
        RuleFor(x => x.VehicleRegistration)
            .NotEmpty()
            .Length(options.Value.VehicleRegistrationNumberLength);
        
        RuleFor(x => x.VehicleType)
            .IsInEnum()
            .WithMessage("Invalid Vehicle Type");
    }
}