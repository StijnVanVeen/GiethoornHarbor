using Microsoft.AspNetCore.Mvc;
using SecurityManagementAPI.Models;

namespace SecurityManagementAPI.IServices
{
	public interface ITruckService
	{
		public Task AddTruckAsync(Truck truck);
		public Task<ActionResult<Truck?>> DepartTruckAsync(int truckId);
		public Task<IEnumerable<Truck>> GetTrucksInHarborAsync();
		public Task<ActionResult<Truck?>> GetTruckByIdAsync(int truckId);
	}
}
