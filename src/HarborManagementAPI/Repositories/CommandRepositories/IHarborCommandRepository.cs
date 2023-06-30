using HarborManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HarborManagementAPI.Repositories;

public interface IHarborCommandRepository
{
    Task AddArrivalAsync(Arrival arrival);
    Task AddDepartureAsync(Departure departure);
    
    Task UpdateArrivalAsync(Arrival arrival);
    Task UpdateDepartureAsync(Departure departure);
    
    Task AddTug(Tugboat tugboat);
    Task DispatchTugAsync(int tugId, int? arrivalId, int? departureId);
    Task<ActionResult<Tugboat>> ReturnTugAsync(int id);
    
    Task AddDock(Dock dock);
    Task FreeDockAsync(int id);
    Task OccupyDockAsync(int id, int? shipId);
}
