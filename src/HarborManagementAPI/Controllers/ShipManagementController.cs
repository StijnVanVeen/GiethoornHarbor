using HarborManagementAPI.Events;
using HarborManagementAPI.Mappers;
using HarborManagementAPI.Models;
using HarborManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipManagementController : ControllerBase
    {
        private readonly IHarborManagementService _service;
        public ShipManagementController(IHarborManagementService service) { 
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentShipsAsync()
        {
            return Ok(await _service.GetShipList());
        }

        [HttpGet]
        [Route("{id}", Name = "GetShipById")]
        public async Task <IActionResult> GetShipbyId(int id)
        {
            var ship = await _service.GetShipById(id);
            if (ship == null)
            {
                return NotFound();
            }
            return Ok(ship);
        }

        [HttpPost]
        public async Task<ActionResult<ShipArrived>> AddShipAsync(ShipArrived requestObject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // add ship
                    Ship ship = requestObject.MapToShip();
                    ship.Arrival = DateTime.Now;
                    await _service.AddShipAsync(ship);

                    // return result
                    return CreatedAtRoute("AddShip", new { ShipId = ship.Id }, ship);
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

        [HttpPost("depart/{id}")]
        public async Task<IActionResult> DepartShipAsync(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // depart ship
                    Ship? ship = (await _service.DepartShipAsync(id)).Value;

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
}
