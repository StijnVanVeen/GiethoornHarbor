using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityManagementAPI.DataAccess;
using SecurityManagementAPI.Models;

namespace SecurityManagementAPI.Services
{
	public class TruckService
	{
		HarborSecurityDbContext _dbContext;
		public TruckService(HarborSecurityDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task AddTruckAsync(Truck truck)
		{
			_dbContext.Trucks.Add(truck);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<ActionResult<Truck?>> DepartTruckAsync(int truckId)
		{
			Truck? truckToDepart = await _dbContext.Trucks.SingleOrDefaultAsync(t => t.Id == truckId);
			if (truckToDepart == null)
			{
				return truckToDepart;
			}
			truckToDepart.DepartureDate = DateTime.Now;
			var truck = _dbContext.Trucks.Attach(truckToDepart);
			truck.State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
			return truckToDepart;
		}

		public async Task<IEnumerable<Truck>> GetTrucksInHarborAsync()
		{
			return await _dbContext.Trucks.Where(truck => truck.DepartureDate != null).ToListAsync();
		}

		public async Task<ActionResult<Truck?>> GetTruckByIdAsync(int truckId)
		{
			return await _dbContext.Trucks.SingleOrDefaultAsync(truck => truck.Id == truckId);
		}
	}
}
