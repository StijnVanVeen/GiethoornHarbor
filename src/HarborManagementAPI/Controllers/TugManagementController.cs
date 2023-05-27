using HarborManagementAPI.Commands;
using HarborManagementAPI.Mappers;
using HarborManagementAPI.Models;
using HarborManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TugManagementController : ControllerBase
    {
        private readonly HarborManagementService _service;

        public TugManagementController(HarborManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTugsAsync()
        {
            return Ok(await _service.GetFreeTugs());
        }

        [HttpGet]
        [Route("{id}", Name = "GetTugById")]
        public async Task<IActionResult> GetTugbyId(int id)
        {
            var tug = await _service.GetTugById(id);
            if (tug == null)
            {
                return NotFound();
            }
            return Ok(tug);
        }

        [HttpPost("dispatch/{id}")]
        public async Task<IActionResult> DispatchTugAsync([FromBody] DispatchTug command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // add tug
                    Tugboat tug = command.MapToTug();
                    await _service.DispatchTugAsync(tug.Id, command.ShipId);

                    // return result
                    return CreatedAtRoute("Dispatch", new { TugId = tug.Id }, tug);
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
        public async Task<IActionResult> DispatchTugAsync(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // depart ship
                    Tugboat? tugboat = (await _service.ReturnTugAsync(id)).Value;

                    // return result
                    return CreatedAtRoute("Return", new { TugId = id }, tugboat);
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
