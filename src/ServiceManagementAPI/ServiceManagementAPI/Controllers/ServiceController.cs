using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagementAPI.Commands;
using ServiceManagementAPI.DataAccess;
using ServiceManagementAPI.Mappers;
using ServiceManagementAPI.Model;

namespace ServiceManagementAPI.Controllers;

[Route("/api/[controller]")]
public class ServicesController : Controller
{
    private ServiceManagementDBContext _context;
    
    public ServicesController(ServiceManagementDBContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _context.Services.ToListAsync());
    }
    
    [HttpGet]
    [Route("{id}", Name = "GetServiceById")]
    public async Task<IActionResult> GetById(string id)
    {
        var service = await _context.Services.FirstOrDefaultAsync(s => s.ID == id.ToString());
        if (service == null)
        {
            return NotFound();
        }
        return Ok(service);
    }
    
    [HttpPost]
    [Route("refuel/{id}", Name = "RefuelShip")]
    public async Task<IActionResult> OfferRefuelAsync([FromBody] OfferRefuelingService command, string id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                IService service = command.MapToRefuelingService();
                _context.Services.Add(service);
                await _context.SaveChangesAsync();

                // send event 

                // return result
                return CreatedAtRoute("GetServiceById", new { id = service.ID }, service);
            }

            return BadRequest();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "see your system administrator.");
            return BadRequest();
        }
    }
    
    [HttpPost]
    [Route("load/{id}", Name = "LoadShip")]
    public async Task<IActionResult> OfferLoadAsync([FromBody] OfferLoadingService command, string id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                IService service = command.MapToLoadingService();
                _context.Services.Add(service);
                await _context.SaveChangesAsync();

                // send event 

                // return result
                return CreatedAtRoute("GetServiceById", new { id = service.ID }, service);
            }

            return BadRequest();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "see your system administrator.");
            return BadRequest();
        }
    }
}

