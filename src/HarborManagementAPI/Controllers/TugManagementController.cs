using HarborManagementAPI.Events;
using HarborManagementAPI.Mappers;
using HarborManagementAPI.Messaging;
using HarborManagementAPI.Models;
using HarborManagementAPI.Repositories;
using HarborManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TugManagementController : ControllerBase
    {
        private readonly IHarborCommandRepository _commandService;
        private readonly IHarborQueryRepository _queryService;
        private readonly IMessagePublisher _messagePublisher;

        public TugManagementController(IHarborCommandRepository harborCommandRepository, IHarborQueryRepository harborQueryRepository ,IMessagePublisher messagePublisher)
        {
            _commandService = harborCommandRepository;
            _queryService = harborQueryRepository;
            _messagePublisher = messagePublisher;
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
                    await _messagePublisher.PublishMessageAsync("TugDispatched", updatedTug, 0);
                    // return result
                    //return CreatedAtRoute("Dispatch", new { TugId = tugboat.Id }, tugboat);
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
                    // return result
                    //return CreatedAtRoute("Dispatch", new { TugId = tugboat.Id }, tugboat);
                    return Ok();
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
                        await _messagePublisher.PublishMessageAsync("ArrivalUpdated", updatedArrival, 0);
                    }
                    else if (tugboat.DepartureId != null)
                    {
                        Departure? departure = await _queryService.GetDepartureAsync(tugboat.DepartureId.Value);
                        await _commandService.FreeDockAsync(departure.DockId);
                    }
                    
                    await _commandService.ReturnTugAsync(id);
                    tugboat.Available = true;
                    tugboat.ArrivalId = null;
                    tugboat.DepartureId = null;
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
