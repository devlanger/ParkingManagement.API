using CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;
using CarAssignment.Application.CQRS.Command.DeallocateVehicleCommand;
using CarAssignment.Application.CQRS.Queries.GetParkingSpacesQuery;
using CarAssignment.Core;
using CarAssignment.Core.Data.Enums;
using CarAssignment.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAssignment.Controllers;

[ApiController]
[Route("parking")]
public class ParkingController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Enter(string vehicleReg, VehicleType vehicleType)
    {
        try
        {
            return Ok(await mediator.Send(new AllocateVehicleCommand(vehicleReg, vehicleType)));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await mediator.Send(new GetParkingSpacesQuery()));

    [HttpPost("exit")]
    public async Task<IActionResult> Exit(string vehicleReg)
    {
        try
        {
            var result = await mediator.Send(new DeallocateVehicleCommand(vehicleReg));
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}