using CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;
using CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;
using CarAssignment.Core;
using CarAssignment.Core.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAssignment.Controllers;

[ApiController]
[Route("parking")]
public class ParkingController(IParkingService parkingService, IMediator mediator) : ControllerBase
{
    [HttpPost]
    public IActionResult Enter(string vehicleReg, VehicleType vehicleType) =>
        Ok(mediator.Send(new AllocateVehicleCommand(vehicleReg, vehicleType)));

    [HttpGet]
    public IActionResult Get() => Ok(new
        {
            AvailableSpaces = parkingService.GetAvailableCapacity(),
            OccupiedSpaces = parkingService.GetOccupiedCapacity()
        });

    [HttpPost("exit")]
    public IActionResult Exit(string vehicleReg) =>
        Ok(mediator.Send(new DeallocateVehicleCommand(vehicleReg)));
}