using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipsManagementAPI.Events;
using ShipsManagementAPI.Mappers;
using ShipsManagementAPI.Messaging;
using ShipsManagementAPI.Model;
using ShipsManagementAPI.Queries;
using ShipsManagementAPI.Writes;

namespace ShipsManagementAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ShipsController : ControllerBase
{
    private readonly IShipWriteRepository _shipWriteRepository;
    private readonly IShipQueryRepository _shipQueryRepository;
    private readonly IMessagePublisher _messagePublisher;
    
    public ShipsController(IShipQueryRepository shipQueryRepository, IShipWriteRepository shipWriteRepository,  IMessagePublisher messagePublisher)
    {
        _shipWriteRepository = shipWriteRepository;
        _shipQueryRepository = shipQueryRepository;
        _messagePublisher = messagePublisher;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _shipQueryRepository.FindAll());
    }
    
    [HttpGet]
    [Route("{id}", Name = "GetShipById")]
    public async Task<IActionResult> GetById(int id)
    {
        var ship = await _shipQueryRepository.FindById(id);
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
                var shipId = await _shipWriteRepository.Insert(ship);

                await _messagePublisher.PublishMessageAsync(requestObject.EventType, ship, 0);
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