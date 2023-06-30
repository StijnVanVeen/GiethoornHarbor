using HarborManagementAPI.Messaging;
using HarborManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HarborManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController: ControllerBase
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public EventController(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    [Route("/events")]
    [HttpGet]
    public async Task<IActionResult> GetEventsAsync()
    {
        return Ok(await _eventStoreRepository.GetEventsAsync());
    }
}