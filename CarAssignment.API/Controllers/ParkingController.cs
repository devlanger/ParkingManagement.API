using CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;
using CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;
using CarAssignment.Application.CQRS.Queries.GetParkingSpacesQuery;
using CarAssignment.Core.Data.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAssignment.API.Controllers;

[ApiController]
[Route("parking")]
public class ParkingController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Enter(string vehicleReg, VehicleType vehicleType)
    {
        return Ok(await mediator.Send(new AllocateVehicleCommand(vehicleReg, vehicleType)));
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await mediator.Send(new GetParkingSpacesQuery()));

    [HttpPost("exit")]
    public async Task<IActionResult> Exit(string vehicleReg)
    {
        return Ok(await mediator.Send(new DeallocateVehicleCommand(vehicleReg)));
    }
    
}