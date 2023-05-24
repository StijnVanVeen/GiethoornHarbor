using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.Services
{

    
    public class HarborManagementService
    {
        HarborManagementDBContext _dbContext;
        public HarborManagementService(HarborManagementDBContext dBContext) {
            _dbContext = dBContext;
        }
        public async Task AddShip(Ship ship)
        {
            _dbContext.Ships.Add(ship);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DepartShip (Ship ship)
        {
            var DepartShip = await _dbContext.Ships.SingleOrDefaultAsync(s => s.Id == ship.Id);
            DepartShip.Departure = DateTime.Now;
            var str = _dbContext.Ships.Attach(DepartShip);
            str.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ship>> GetShipList()
        {
            return await _dbContext.Ships.Where(ship => ship.Departure != null).ToListAsync();
        }

        public async Task<ActionResult<Ship>> GetShipById (int Id)
        {
            return await _dbContext.Ships.SingleOrDefaultAsync(ship => ship.Id == Id);
        }

        public async Task AddTug (Tugboat tugboat)
        {
            _dbContext.Tugboat.Add(tugboat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tugboat>> GetFreeTugs ()
        {
            return await _dbContext.Tugboat.Where(tug => tug.Available == true).ToListAsync();
        }

        public async Task DispatchTug (int TugId, int ShipId) {
            var tug = await _dbContext.Tugboat.SingleOrDefaultAsync(s => s.Id == TugId);
            tug.Available = false;
            tug.ShipId = ShipId;
            _dbContext.Tugboat.Update(tug);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ReturnTug (int Id)
        {
            var tug = await _dbContext.Tugboat.SingleOrDefaultAsync(s => s.Id == Id);
            tug.Available = true;
            _dbContext.Tugboat.Update(tug);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddDock (Dock dock) {
            _dbContext.Docks.Add(dock);
            await _dbContext.SaveChangesAsync();
        }


    }
}
