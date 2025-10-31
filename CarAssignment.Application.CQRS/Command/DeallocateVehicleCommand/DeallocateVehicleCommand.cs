using MediatR;

namespace CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;

public record DeallocateVehicleCommand(string VehicleRegistration) : IRequest;