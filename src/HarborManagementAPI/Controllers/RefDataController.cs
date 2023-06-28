using HarborManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HarborManagementAPI.Controllers;

[Route("/api/[controller]")]
public class RefDataController : Controller
{
    
    //update to Ship
    IShipRepository _shipRepo;

    public RefDataController(IShipRepository shipRepo)
    {
        _shipRepo = shipRepo;
    }

    [HttpGet]
    [Route("ships")]
    public async Task<IActionResult> GetShips()
    {
        return Ok(await _shipRepo.GetShipsAsync());
    }

    [HttpGet]
    [Route("ships/{shipsId}")]
    public async Task<IActionResult> GetShipById(int shipId)
    {
        var ship = await _shipRepo.GetShipAsync(shipId);
        if (ship == null)
        {
            return NotFound();
        }
        return Ok(ship);
    }
}