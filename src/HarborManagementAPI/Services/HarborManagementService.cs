﻿/*
using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.Services
{

    
    public class HarborManagementService : IHarborManagementService
    {
        HarborManagementDBContext _dbContext;
        public HarborManagementService(HarborManagementDBContext dBContext) {
            _dbContext = dBContext;
        }
        public async Task AddShipAsync(Ship ship)
        {
            _dbContext.Ships.Add(ship);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Ship>> DepartShipAsync (int id)
        {
            var DepartShip = await _dbContext.Ships.SingleOrDefaultAsync(s => s.Id == id);
            //DepartShip.Departure = DateTime.Now;
            var str = _dbContext.Ships.Attach(DepartShip);
            str.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return DepartShip;
        }

        public async Task<IEnumerable<Ship>> GetShipList()
        {
            return await _dbContext.Ships.Where(ship => ship.Brand != null).ToListAsync();
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

        public async Task<ActionResult<Tugboat>> GetTugById(int Id)
        {
            return await _dbContext.Tugboat.SingleOrDefaultAsync(tug => tug.Id == Id);
        }

        public async Task DispatchTugAsync(int TugId, int? arrivalId, int? departureId) {
            var tug = await _dbContext.Tugboat.SingleOrDefaultAsync(s => s.Id == TugId);
            tug.Available = false;
            tug.ArrivalId = arrivalId;
            tug.DepartureId = departureId;
            _dbContext.Tugboat.Update(tug);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Tugboat>> ReturnTugAsync (int Id)
        {
            var tug = await _dbContext.Tugboat.SingleOrDefaultAsync(s => s.Id == Id);
            tug.Available = true;
            tug.ArrivalId = null;
            tug.DepartureId = null;
            _dbContext.Tugboat.Update(tug);
            await _dbContext.SaveChangesAsync();
            return tug;
        }

        public async Task AddDock (Dock dock) {
            _dbContext.Docks.Add(dock);
            await _dbContext.SaveChangesAsync();
        }
        
    }
}
*/
