using HarborManagementAPI.Events;
using HarborManagementAPI.Mappers;
using HarborManagementAPI.Messaging;
using HarborManagementAPI.Models;
using HarborManagementAPI.Repositories;
using HarborManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HarborManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TugManagementController : ControllerBase
    {
        private readonly IHarborCommandRepository _commandService;
        private readonly IHarborQueryRepository _queryService;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IEventStoreRepository _eventStoreRepository;

        public TugManagementController(IHarborCommandRepository harborCommandRepository, IHarborQueryRepository harborQueryRepository ,IMessagePublisher messagePublisher, IEventStoreRepository eventStoreRepository)
        {
            _commandService = harborCommandRepository;
            _queryService = harborQueryRepository;
            _messagePublisher = messagePublisher;
            _eventStoreRepository = eventStoreRepository;
        }

        [Route("/tugs/free")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableTugsAsync()
        {
            return Ok(await _queryService.GetFreeTugs());
        }
        
        [Route("/tugs/busy")]
        [HttpGet]
        public async Task<IActionResult> GetBusyTugsAsync()
        {
            return Ok(await _queryService.GetBusyTugs());
        }

        [HttpGet]
        [Route("{id}", Name = "GetTugById")]
        public async Task<IActionResult> GetTugbyId(int id)
        {
            var tug = await _queryService.GetTugById(id);
            if (tug == null)
            {
                return NotFound();
            }
            return Ok(tug);
        }
        
        
        [HttpPost("tugboat")]
        public async Task<ActionResult<Tugboat>> CreateTugboatAsync(TugRegistered requestObject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tugboat tugboat = requestObject.MapToTug();
                    await _commandService.AddTug(tugboat);
                    StoreEvent ev = new StoreEvent(requestObject.EventType, JsonSerializer.Serialize(requestObject),
                        Guid.NewGuid().ToString());
                    await _eventStoreRepository.AddEventAsync(ev);
                    await _messagePublisher.PublishMessageAsync(requestObject.EventType, tugboat, 0);
                    // return result
                    //return CreatedAtRoute("GetTugById", new { id = tugboat.Id }, tugboat);
                    return Ok(tugboat);
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
        

        [HttpPost("dispatch/{id}/arrival/{arrivalId}")]
        public async Task<ActionResult<Tugboat>> DispatchTugArrivalAsync(int id, int arrivalId )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tugboat? tugboat = await _queryService.GetTugById(id);
                    if (tugboat == null)
                    {
                        return NotFound();
                    }

                    Arrival? arrival = await _queryService.GetArrivalAsync(arrivalId);
                    if (arrival == null)
                    {
                        return NotFound();
                    }
                    
                    await _commandService.DispatchTugAsync(tugboat.Id, arrival.Id, null);
                    var updatedTug = new Tugboat
                    {
                        Id = tugboat.Id,
                        Name = tugboat.Name,
                        Available = false,
                        ArrivalId = arrival.Id,
                        DepartureId = null
                    };
                    StoreEvent ev = new StoreEvent("TugDispatched", JsonSerializer.Serialize(new TugDispatched(false, arrival.ShipId)),
                        Guid.NewGuid().ToString());
                    await _eventStoreRepository.AddEventAsync(ev);
                    await _messagePublisher.PublishMessageAsync("TugDispatched", updatedTug, 0);
                    // return result
                    return Ok(updatedTug);
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
        
        [HttpPost("dispatch/{id}/departure/{departureId}")]
        public async Task<ActionResult<Tugboat>> DispatchTugDepartureAsync(int id, int departureId )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tugboat? tugboat = await _queryService.GetTugById(id);
                    if (tugboat == null)
                    {
                        return NotFound();
                    }

                    Departure? departure = await _queryService.GetDepartureAsync(departureId);
                    if (departure == null)
                    {
                        return NotFound();
                    }

                    await _commandService.DispatchTugAsync(tugboat.Id, null, departureId);
                    var updatedTug = new Tugboat
                    {
                        Id = tugboat.Id,
                        Name = tugboat.Name,
                        Available = false,
                        ArrivalId = null,
                        DepartureId = departure.Id
                    };
                    StoreEvent ev = new StoreEvent("TugDispatched", JsonSerializer.Serialize(tugboat),
                        Guid.NewGuid().ToString());
                    await _eventStoreRepository.AddEventAsync(ev);
                    // return result
                    await _messagePublisher.PublishMessageAsync("TugDispatched", updatedTug, 0);
                    return Ok(updatedTug);
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

        [HttpPost("return/{id}")]
        public async Task<IActionResult> ReturnTugAsync(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // depart ship
                    Tugboat? tugboat = await _queryService.GetTugById(id);
                    if (tugboat == null)
                    {
                        return NotFound();
                    }

                    if (tugboat.ArrivalId != null)
                    {
                        Arrival? arrival = await _queryService.GetArrivalAsync(tugboat.ArrivalId.Value);
                        await _commandService.OccupyDockAsync(arrival.DockId, arrival.ShipId);
                        Dock? dock = await _queryService.GetDockAsync(arrival.DockId);
                        dock.Available = false;
                        dock.ShipId = arrival.ShipId;
                        StoreEvent ev = new StoreEvent("DockUpdated", JsonSerializer.Serialize(dock),
                            Guid.NewGuid().ToString());
                        await _eventStoreRepository.AddEventAsync(ev);
                        await _messagePublisher.PublishMessageAsync("DockUpdated", dock, 0);
                        Arrival updatedArrival = new Arrival
                        {
                            Id = arrival.Id,
                            ShipId = arrival.ShipId,
                            DockId = arrival.DockId,
                            ArrivalDate = arrival.ArrivalDate,
                            IsDocked = true
                        };
                        await _commandService.UpdateArrivalAsync(updatedArrival);
                        StoreEvent ev2 = new StoreEvent("ArrivalUpdated", JsonSerializer.Serialize(arrival),
                            Guid.NewGuid().ToString());
                        await _eventStoreRepository.AddEventAsync(ev2);
                        await _messagePublisher.PublishMessageAsync("ArrivalUpdated", updatedArrival, 0);
                    }
                    else if (tugboat.DepartureId != null)
                    {
                        Departure? departure = await _queryService.GetDepartureAsync(tugboat.DepartureId.Value);
                        await _commandService.FreeDockAsync(departure.DockId);
                        Dock? dock = await _queryService.GetDockAsync(departure.DockId);
                        dock.Available = true;
                        dock.ShipId = null;
                        StoreEvent ev3 = new StoreEvent("DockUpdated", JsonSerializer.Serialize(dock),
                            Guid.NewGuid().ToString());
                        await _eventStoreRepository.AddEventAsync(ev3);
                        await _messagePublisher.PublishMessageAsync("DockUpdated", dock, 0);
                        Departure updatedDeparture = new Departure
                        {
                            Id = departure.Id,
                            ShipId = departure.ShipId,
                            DockId = departure.DockId,
                            DepartureDate = departure.DepartureDate,
                            LeftHarbor = true
                        };
                        await _commandService.UpdateDepartureAsync(updatedDeparture);
                        StoreEvent ev4 = new StoreEvent("DepartureUpdated", JsonSerializer.Serialize(departure),
                            Guid.NewGuid().ToString());
                        await _eventStoreRepository.AddEventAsync(ev4);
                        await _messagePublisher.PublishMessageAsync("DepartureUpdated", updatedDeparture, 0);
                    }
                    
                    await _commandService.ReturnTugAsync(id);
                    tugboat.Available = true;
                    tugboat.ArrivalId = null;
                    tugboat.DepartureId = null;
                    StoreEvent ev5 = new StoreEvent("TugReturned", JsonSerializer.Serialize(tugboat),
                        Guid.NewGuid().ToString());
                    await _eventStoreRepository.AddEventAsync(ev5);
                    await _messagePublisher.PublishMessageAsync("TugReturned", tugboat, 0);
                    // return result
                    return Ok(tugboat);
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
}
