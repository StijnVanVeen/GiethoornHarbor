using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipsManagementAPI.Events;
using ShipsManagementAPI.Mappers;
using ShipsManagementAPI.Messaging;
using ShipsManagementAPI.Model;
using ShipsManagementAPI.RepoServices;

namespace ShipsManagementAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ShipsController : ControllerBase
{
    private readonly IShipRepoService _shipRepoService;
    private readonly IMessagePublisher _messagePublisher;
    
    public ShipsController(IShipRepoService shipRepoService, IMessagePublisher messagePublisher)
    {
        _shipRepoService = shipRepoService;
        _messagePublisher = messagePublisher;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _shipRepoService.FindAll());
    }
    
    [HttpGet]
    [Route("{id}", Name = "GetShipById")]
    public async Task<IActionResult> GetById(int id)
    {
        var ship = await _shipRepoService.FindById(id);
        if (ship == null)
        {
            return NotFound();
        }
        return Ok(ship);
    }
    
    [HttpPost]
    public async Task<ActionResult<ShipRegistered>> RegisterAsync(ShipRegistered requestObject)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Ship ship = requestObject.MapToShip();
                var shipId = await _shipRepoService.Insert(ship);

                await _messagePublisher.PublishMessageAsync(requestObject.EventType, ship, 2);
                return Ok(ship);
            }

            return BadRequest();
        }
        catch (DbUpdateException e)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "see your system administrator.");
            Console.WriteLine(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}