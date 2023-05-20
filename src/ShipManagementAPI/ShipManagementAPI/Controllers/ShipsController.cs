using Microsoft.AspNetCore.Mvc;
using ShipManagementAPI.Commands;

namespace ShipManagementAPI.Controllers;

[Route("api/[controller]")]
public class ShipsController : Controller
{
    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id}", Name = "GetShipById")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterShip command)
    {
        return Ok();
    }
}