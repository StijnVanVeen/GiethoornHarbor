using HarborManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HarborManagementAPI.Services
{
    public interface IHarborManagementService
    {
        public Task AddShipAsync(Ship ship);
        public Task<ActionResult<Ship>> DepartShipAsync(int id);
        public Task<IEnumerable<Ship>> GetShipList();
        public Task<ActionResult<Ship>> GetShipById(int Id);
        public Task AddTug(Tugboat tugboat);
        public Task<IEnumerable<Tugboat>> GetFreeTugs();
        public Task<ActionResult<Tugboat>> GetTugById(int Id);
        public Task DispatchTugAsync(int TugId, int? ShipId);
        public Task<ActionResult<Tugboat>> ReturnTugAsync(int Id);
        public Task AddDock(Dock dock);
    }
}
