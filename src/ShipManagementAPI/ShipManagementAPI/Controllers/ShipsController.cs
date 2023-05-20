using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipManagementAPI.Commands;
using ShipManagementAPI.DataAccess;
using ShipManagementAPI.Mappers;
using ShipManagementAPI.Model;

namespace ShipManagementAPI.Controllers;

[Route("/api/[controller]")]
public class ShipsController : Controller
{
    ShipManagementDBContext _context;
    
    public ShipsController(ShipManagementDBContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _context.Ships.ToListAsync());
    }
    
    [HttpGet]
    [Route("{id}", Name = "GetShipById")]
    public async Task<IActionResult> GetById(int id)
    {
        var ship = await _context.Ships.FirstOrDefaultAsync(s => s.ID == id.ToString());
        if (ship == null)
        {
            return NotFound();
        }
        return Ok(ship);
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterShip command)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Ship ship = command.MapToShip();
                _context.Ships.Add(ship);
                await _context.SaveChangesAsync();

                // send event 

                // return result
                return CreatedAtRoute("GetShipById", new { id = ship.ID }, ship);
            }

            return BadRequest();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "see your system administrator.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}