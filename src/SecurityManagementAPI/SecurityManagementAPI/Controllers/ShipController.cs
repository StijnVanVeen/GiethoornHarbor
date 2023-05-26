using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityManagementAPI.Commands;
using SecurityManagementAPI.Mappers;
using SecurityManagementAPI.Models;
using SecurityManagementAPI.Services;

namespace SecurityManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipController : ControllerBase
{
	private readonly ShipService _shipService;
	public ShipController(ShipService service)
	{
		_shipService = service;
	}

	[HttpGet]
	public async Task<IEnumerable<Ship>> GetDockedShipsAsync()
	{
		return await _shipService.GetDockedShipsAsync();
	}

	[HttpGet("{id}")]
	public async Task<Ship?> GetShipByIdAsync(int id)
	{
		return (await _shipService.GetShipByIdAsync(id)).Value;
	}

	[HttpPost]
	public async Task<IActionResult> AddShipAsync([FromBody] AddShip command)
	{
		try
		{
			if (ModelState.IsValid)
			{
				// add ship
				Ship ship = command.MapToShip();
				await _shipService.AddShipAsync(ship);

				// return result
				return CreatedAtRoute("AddShip", new { ShipId = ship.Id }, ship);
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
	public async Task<IActionResult> DepartShipAsync(int id)
	{
		try
		{
			if (ModelState.IsValid)
			{
				// depart ship
				Ship? ship = (await _shipService.DepartShipAsync(id)).Value;

				// return result
				return CreatedAtRoute("Depart", new { ShipId = id }, ship);
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