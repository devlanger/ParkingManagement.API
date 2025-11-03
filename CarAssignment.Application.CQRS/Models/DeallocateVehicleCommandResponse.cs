namespace CarAssignment.Application.CQRS.Models;

public record DeallocateVehicleCommandResponse(string VehicleReg, double VehicleCharge, DateTimeOffset DateIn, DateTimeOffset DateOut);