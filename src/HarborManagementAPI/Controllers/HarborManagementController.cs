﻿using HarborManagementAPI.Events;
using HarborManagementAPI.Mappers;
using HarborManagementAPI.Messaging;
using HarborManagementAPI.Models;
using HarborManagementAPI.Repositories;
using HarborManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Serilog;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HarborManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarborManagementController : ControllerBase
    {
        private readonly IHarborCommandRepository _commandService;
        private readonly IHarborQueryRepository _queryService;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IShipQueryRepository _shipQueryRepository;
        
        public HarborManagementController(IHarborCommandRepository harborCommandRepository, IHarborQueryRepository harborQueryRepository ,IMessagePublisher messagePublisher, IEventStoreRepository eventStoreRepository, IShipQueryRepository shipQueryRepository){ 
            _commandService = harborCommandRepository;
            _queryService = harborQueryRepository;
            _messagePublisher = messagePublisher;
            _eventStoreRepository = eventStoreRepository;
            _shipQueryRepository = shipQueryRepository;
        }

        [HttpGet]
        [Route("/arrivals")]
        public async Task<IActionResult> GetAllArrivalsAsync()
        {
            return Ok(await _queryService.GetArrivalsAsync());
        }
        
        [HttpGet]
        [Route("/departures")]
        public async Task<IActionResult> GetAllDeparturesAsync()
        {
            return Ok(await _queryService.GetDeparturesAsync());
        }
        
        [HttpGet]
        [Route("/arrivals/{id}", Name = "GetArrivalById")]
        public async Task <IActionResult> GetArrivalById(int id)
        {
            var arrival = await _queryService.GetArrivalAsync(id);
            if (arrival == null)
            {
                return NotFound();
            }
            return Ok(arrival);
        }
        
        [HttpGet]
        [Route("/departures/{id}", Name = "GetDepartureById")]
        public async Task <IActionResult> GetDepartureById(int id)
        {
            var departure = await _queryService.GetDepartureAsync(id);
            if (departure == null)
            {
                return NotFound();
            }
            return Ok(departure);
        }
        
        [HttpPost]
        [Route("/arrivals")]
        public async Task<IActionResult> AddArrivalAsync(ShipArrived requestObject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Dock? dock = await _queryService.GetDockAsync(requestObject.DockId);
                    Ship? ship = await _shipQueryRepository.GetShipAsync(requestObject.ShipId);
                    // should check ship too
                    if (dock != null && dock.Available && ship != null)
                    {
                        Arrival arrival = requestObject.MapToArrival();
                        await _commandService.AddArrivalAsync(arrival);

                        StoreEvent ev = new StoreEvent(requestObject.EventType, JsonSerializer.Serialize(requestObject),
                            Guid.NewGuid().ToString());
                        await _eventStoreRepository.AddEventAsync(ev);
                        await _messagePublisher.PublishMessageAsync(requestObject.EventType, arrival, 0);

                        return Ok(arrival);
                    }
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
        
        [HttpPost]
        [Route("/departures")]
        public async Task<IActionResult> AddDepartureAsync(ShipDeparted requestObject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var arrivals = await _queryService.GetArrivalsAsync();
                    Arrival? tempArival = arrivals.FirstOrDefault(a => a.ShipId == requestObject.ShipId && a.DockId == requestObject.DockId && a.IsDocked);
                    if (tempArival == null)
                    {
                        return BadRequest();
                    }
                    Departure departure = requestObject.MapToDeparture();
                    await _commandService.AddDepartureAsync(departure);
                    StoreEvent ev = new StoreEvent(requestObject.EventType, JsonSerializer.Serialize(requestObject),
                        Guid.NewGuid().ToString());
                    await _eventStoreRepository.AddEventAsync(ev);
                    await _messagePublisher.PublishMessageAsync(requestObject.EventType, departure, 0);
                    //return CreatedAtRoute("GetDepartureById", new { id = departure.Id }, departure);
                    return Ok(departure);
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
}
