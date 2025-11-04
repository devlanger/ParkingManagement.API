namespace CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;

public record AllocateVehicleCommandResponse(string VehicleReg, int SpaceNumber, DateTimeOffset TimeIn);
