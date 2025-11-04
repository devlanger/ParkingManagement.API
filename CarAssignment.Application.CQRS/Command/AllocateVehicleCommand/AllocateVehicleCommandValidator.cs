using FluentValidation;

namespace CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;

public class AllocateVehicleCommandValidator : AbstractValidator<AllocateVehicleCommand>
{
    public AllocateVehicleCommandValidator()
    {
        RuleFor(x => x.VehicleRegistration)
            .NotEmpty()
            .Length(7);
        
        RuleFor(x => x.VehicleType)
            .IsInEnum()
            .WithMessage("Invalid Vehicle Type");
    }
}