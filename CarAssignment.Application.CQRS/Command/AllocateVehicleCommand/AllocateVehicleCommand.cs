using CarAssignment.Core;
using MediatR;

namespace CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;

public record AllocateVehicleCommand(string VehicleRegistration, VehicleType VehicleType) : IRequest;