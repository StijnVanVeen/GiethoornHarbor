using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityManagementAPI.Commands;
using SecurityManagementAPI.Events;
using SecurityManagementAPI.IServices;
using SecurityManagementAPI.Mappers;
using SecurityManagementAPI.Models;

namespace SecurityManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TruckController : ControllerBase
{
	private readonly ITruckService _truckService;
	private readonly IShipService _shipService;
	public TruckController(ITruckService service, IShipService shipService)
	{
		_truckService = service;
		_shipService = shipService;
	}

	[HttpGet]
	public async Task<IEnumerable<Truck>> GetTrucksInHarborAsync()
	{
		return await _truckService.GetTrucksInHarborAsync();
	}

	[HttpGet("{id}")]
	public async Task<Truck?> GetTruckByIdAsync(int id)
	{
		return (await _truckService.GetTruckByIdAsync(id)).Value;
	}

	[HttpPost]
	public async Task<IActionResult> AddTruckAsync([FromBody] AddTruck command)
	{
		try
		{
			if (ModelState.IsValid)
			{
				// add truck
				Truck truck = command.MapToTruck();
				Ship? ship = (await _shipService.GetShipByIdAsync(truck.ShipId)).Value;

				if (ship != null && ship.DepartureDate != null)
				{
					await _truckService.AddTruckAsync(truck);

					// send Access Granted event
					AccessGranted e = command.MapToAccessGranted(ship.DockId);
					//await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");
				}
				else
				{
					truck.DepartureDate = truck.ArrivalDate;
					await _truckService.AddTruckAsync(truck);

					// send Access Denied event
					AccessDenied e = command.MapToAccessDenied();
					//await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");
				}

				// return result
				return CreatedAtRoute("AddTruck", new { TruckId = truck.Id }, truck);
			}
			return BadRequest();
		}
		catch (DbUpdateException)
		{
			ModelState.AddModelError("", "Unable to save changes. " +
				"Try again, and if the problem persists " +
				"see your system administrator.");
			return StatusCode(StatusCodes.Status500InternalServerError);
			throw;
		}
	}

	[HttpPost("depart/{id}")]
	public async Task<IActionResult> DepartTruckAsync(int id)
	{
		try
		{
			if (ModelState.IsValid)
			{
				// depart truck
				Truck? truck = (await _truckService.DepartTruckAsync(id)).Value;

				// return result
				return CreatedAtRoute("Depart", new { TruckId = id }, truck);
			}
			return BadRequest();
		}
		catch (DbUpdateException)
		{
			ModelState.AddModelError("", "Unable to save changes. " +
				"Try again, and if the problem persists " +
				"see your system administrator.");
			return StatusCode(StatusCodes.Status500InternalServerError);
			throw;
		}
	}
}