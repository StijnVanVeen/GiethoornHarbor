using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging;
using ShipsManagementAPI.Commands;
using ShipsManagementAPI.DataAccess;
using ShipsManagementAPI.Events;
using ShipsManagementAPI.Mappers;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Controllers;

[Route("/api/[controller]")]
public class ShipsController : Controller
{
    ShipsManagementDBContext _context;
    IMessagePublisher _messagePublisher;
    
    public ShipsController(ShipsManagementDBContext context, IMessagePublisher messagePublisher)
    {
        _context = context;
        _messagePublisher = messagePublisher;
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
        var ship = await _context.Ships.FirstOrDefaultAsync(s => s.Id == id.ToString());
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
                var e = ShipRegistered.FromCommand(command);
                await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");
                // return result
                return CreatedAtRoute("GetShipById", new { id = ship.Id }, ship);
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