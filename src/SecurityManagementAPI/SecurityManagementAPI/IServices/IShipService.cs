using Microsoft.AspNetCore.Mvc;
using SecurityManagementAPI.Models;

namespace SecurityManagementAPI.IServices
{
	public interface IShipService
	{
		public Task AddShipAsync(Ship ship);
		public Task<ActionResult<Ship?>> DepartShipAsync(int shipId);
		public Task<IEnumerable<Ship>> GetDockedShipsAsync();
		public Task<ActionResult<Ship?>> GetShipByIdAsync(int shipId);

	}
}
