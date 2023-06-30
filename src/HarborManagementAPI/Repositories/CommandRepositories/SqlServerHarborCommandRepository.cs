using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.Repositories;

public class SqlServerHarborCommandRepository : IHarborCommandRepository
{
    public readonly HarborManagementSQLDBContext SqldbContext;
    
    public SqlServerHarborCommandRepository(HarborManagementSQLDBContext sqldbContext)
    {
        SqldbContext = sqldbContext;
    }

    public async Task AddArrivalAsync(Arrival arrival)
    {
        SqldbContext.Arrivals.Add(arrival);
        await SqldbContext.SaveChangesAsync();
    }

    public async Task AddDepartureAsync(Departure departure)
    {
        SqldbContext.Departures.Add(departure);
        await SqldbContext.SaveChangesAsync();
    }

    public async Task UpdateArrivalAsync(Arrival arrival)
    {
        SqldbContext.Arrivals.Update(arrival);
        await SqldbContext.SaveChangesAsync();
    }

    public async Task UpdateDepartureAsync(Departure departure)
    {
        SqldbContext.Departures.Update(departure);
        await SqldbContext.SaveChangesAsync();
    }

    public async Task AddTug(Tugboat tugboat)
    {
        SqldbContext.Tugboat.Add(tugboat);
        await SqldbContext.SaveChangesAsync();
    }
    
    public async Task DispatchTugAsync(int tugId, int? arrivalId, int? departureId)
    {
        var tug = await SqldbContext.Tugboat.SingleOrDefaultAsync(s => s.Id == tugId);
        tug.Available = false;
        tug.ArrivalId = arrivalId;
        tug.DepartureId = departureId;
        SqldbContext.Tugboat.Update(tug);
        await SqldbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<Tugboat>> ReturnTugAsync(int id)
    {
        var tug = await SqldbContext.Tugboat.SingleOrDefaultAsync(s => s.Id == id);
        tug.Available = true;
        tug.ArrivalId = null;
        tug.DepartureId = null;
        SqldbContext.Tugboat.Update(tug);
        await SqldbContext.SaveChangesAsync();
        return tug;
    }

    public async Task AddDock(Dock dock)
    {
        SqldbContext.Docks.Add(dock);
        await SqldbContext.SaveChangesAsync();
    }

    public async Task FreeDockAsync(int id)
    {
        var dock = await SqldbContext.Docks.SingleOrDefaultAsync(s => s.Id == id);
        if (dock != null)
        {
            dock.Available = true;
            dock.ShipId = null;
            SqldbContext.Docks.Update(dock);
            await SqldbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Dock not found");
        }
    }

    public async Task OccupyDockAsync(int dockId, int? shipId)
    {
        var dock = await SqldbContext.Docks.SingleOrDefaultAsync(s => s.Id == dockId);

        if (dock != null)
        {
            dock.Available = false;
            dock.ShipId = shipId;
            SqldbContext.Docks.Update(dock);
            await SqldbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Dock not found");
        }
    }
}