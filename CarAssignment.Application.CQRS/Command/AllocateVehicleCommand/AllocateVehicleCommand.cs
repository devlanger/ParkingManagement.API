using CarAssignment.Core;
using CarAssignment.Core.Data.Enums;
using MediatR;

namespace CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;

public record AllocateVehicleCommand(string VehicleRegistration, VehicleType VehicleType) : IRequest<AllocateVehicleCommandResponse>;