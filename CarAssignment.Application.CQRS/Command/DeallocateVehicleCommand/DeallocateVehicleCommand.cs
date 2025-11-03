using CarAssignment.Application.CQRS.Models;
using MediatR;

namespace CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;

public record DeallocateVehicleCommand(string VehicleRegistration) : IRequest<DeallocateVehicleCommandResponse>;