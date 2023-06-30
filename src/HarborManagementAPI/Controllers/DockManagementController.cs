using HarborManagementAPI.Events;
using HarborManagementAPI.Mappers;
using HarborManagementAPI.Messaging;
using HarborManagementAPI.Models;
using HarborManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HarborManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DockManagementController : ControllerBase
{
    private readonly IHarborCommandRepository _commandService;
    private readonly IHarborQueryRepository _queryService;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IEventStoreRepository _eventStoreRepository;
    public DockManagementController(IHarborCommandRepository harborCommandRepository, IHarborQueryRepository harborQueryRepository ,IMessagePublisher messagePublisher, IEventStoreRepository eventStoreRepository) { 
        _commandService = harborCommandRepository;
        _queryService = harborQueryRepository;
        _messagePublisher = messagePublisher;
        _eventStoreRepository = eventStoreRepository;
    }
    
    [HttpGet]
    [Route("/docks")]
    public async Task<IActionResult> GetAllDocksAsync()
    {
        return Ok(await _queryService.GetDocksAsync());
    }
    
    [HttpGet]
    [Route("/docks/free")]
    public async Task<IActionResult> GetAllFreeDocksAsync()
    {
        return Ok(await _queryService.GetFreeDocksAsync());
    }
    
    [HttpGet]
    [Route("/docks/busy")]
    public async Task<IActionResult> GetAllBusyDocksAsync()
    {
        return Ok(await _queryService.GetBusyDocksAsync());
    }
    
    [HttpGet]
    [Route("/docks/{id}", Name = "GetDockById")]
    public async Task <IActionResult> GetDockById(int id)
    {
        var dock = await _queryService.GetDockAsync(id);
        if (dock == null)
        {
            return NotFound();
        }
        return Ok(dock);
    }
    
    [HttpPost("/docks")]
    public async Task<ActionResult<DockRegistered>> AddDockAsync(DockRegistered requestObject)
    {
        Console.WriteLine("AddDockAsync");
        try
        {
            if (ModelState.IsValid)
            {
                // add dock
                Dock? dock = requestObject.MapToDock();
                await _commandService.AddDock(dock);
                StoreEvent ev = new StoreEvent(requestObject.EventType, JsonSerializer.Serialize(requestObject),
                    Guid.NewGuid().ToString());
                await _eventStoreRepository.AddEventAsync(ev);
                await _messagePublisher.PublishMessageAsync(requestObject.EventType, dock, 0);

                return Ok(dock);
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