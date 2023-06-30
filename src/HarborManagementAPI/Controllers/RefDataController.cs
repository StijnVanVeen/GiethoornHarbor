using HarborManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HarborManagementAPI.Controllers;

[Route("/api/[controller]")]
public class RefDataController : Controller
{
    
    //update to Ship
    IShipQueryRepository _shipQueryRepo;

    public RefDataController(IShipQueryRepository shipQueryRepo)
    {
        _shipQueryRepo = shipQueryRepo;
    }

    [HttpGet]
    [Route("ships")]
    public async Task<IActionResult> GetShips()
    {
        return Ok(await _shipQueryRepo.GetShipsAsync());
    }

    [HttpGet]
    [Route("ships/{shipsId}")]
    public async Task<IActionResult> GetShipById(int shipId)
    {
        var ship = await _shipQueryRepo.GetShipAsync(shipId);
        if (ship == null)
        {
            return NotFound();
        }
        return Ok(ship);
    }
}