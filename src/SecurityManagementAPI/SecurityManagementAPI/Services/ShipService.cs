using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityManagementAPI.DataAccess;
using SecurityManagementAPI.Models;

namespace SecurityManagementAPI.Services
{
	public class ShipService
	{
		HarborSecurityDbContext _dbContext;
		public ShipService(HarborSecurityDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task AddShipAsync(Ship ship)
		{
			_dbContext.Ships.Add(ship);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<ActionResult<Ship?>> DepartShipAsync(int shipId)
		{
			Ship? shipToDepart = await _dbContext.Ships.SingleOrDefaultAsync(s => s.Id == shipId);
			if (shipToDepart == null)
			{
				return shipToDepart;
			}
			shipToDepart.DepartureDate = DateTime.Now;
			var ship = _dbContext.Ships.Attach(shipToDepart);
			ship.State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
			return shipToDepart;
		}

		public async Task<IEnumerable<Ship>> GetDockedShipsAsync()
		{
			return await _dbContext.Ships.Where(ship => ship.DepartureDate != null).ToListAsync();
		}

		public async Task<ActionResult<Ship?>> GetShipByIdAsync(int shipId)
		{
			return await _dbContext.Ships.SingleOrDefaultAsync(ship => ship.Id == shipId);
		}
	}
}
