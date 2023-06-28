using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.Repositories;

public class SqlServerHarborCommandRepository : IHarborCommandRepository
{
    public readonly HarborManagementDBContext _DbContext;
    
    public SqlServerHarborCommandRepository(HarborManagementDBContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task AddArrivalAsync(Arrival arrival)
    {
        _DbContext.Arrivals.Add(arrival);
        await _DbContext.SaveChangesAsync();
    }

    public async Task AddDepartureAsync(Departure departure)
    {
        _DbContext.Departures.Add(departure);
        await _DbContext.SaveChangesAsync();
    }

    public async Task UpdateArrivalAsync(Arrival arrival)
    {
        _DbContext.Arrivals.Update(arrival);
        await _DbContext.SaveChangesAsync();
    }

    public async Task UpdateDepartureAsync(Departure departure)
    {
        _DbContext.Departures.Update(departure);
        await _DbContext.SaveChangesAsync();
    }

    public async Task AddTug(Tugboat tugboat)
    {
        _DbContext.Tugboat.Add(tugboat);
        await _DbContext.SaveChangesAsync();
    }
    
    public async Task DispatchTugAsync(int tugId, int? arrivalId, int? departureId)
    {
        var tug = await _DbContext.Tugboat.SingleOrDefaultAsync(s => s.Id == tugId);
        tug.Available = false;
        tug.ArrivalId = arrivalId;
        tug.DepartureId = departureId;
        _DbContext.Tugboat.Update(tug);
        await _DbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<Tugboat>> ReturnTugAsync(int id)
    {
        var tug = await _DbContext.Tugboat.SingleOrDefaultAsync(s => s.Id == id);
        tug.Available = true;
        tug.ArrivalId = null;
        tug.DepartureId = null;
        _DbContext.Tugboat.Update(tug);
        await _DbContext.SaveChangesAsync();
        return tug;
    }

    public async Task AddDock(Dock dock)
    {
        _DbContext.Docks.Add(dock);
        await _DbContext.SaveChangesAsync();
    }

    public async Task FreeDockAsync(int id)
    {
        var dock = await _DbContext.Docks.SingleOrDefaultAsync(s => s.Id == id);
        if (dock != null)
        {
            dock.Available = true;
            dock.ShipId = null;
            _DbContext.Docks.Update(dock);
            await _DbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Dock not found");
        }
    }

    public async Task OccupyDockAsync(int dockId, int? shipId)
    {
        var dock = await _DbContext.Docks.SingleOrDefaultAsync(s => s.Id == dockId);

        if (dock != null)
        {
            dock.Available = false;
            dock.ShipId = shipId;
            _DbContext.Docks.Update(dock);
            await _DbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Dock not found");
        }
    }
}