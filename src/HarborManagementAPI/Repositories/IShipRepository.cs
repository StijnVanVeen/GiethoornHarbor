using HarborManagementAPI.Models;

namespace HarborManagementAPI.Repositories;

public interface IShipRepository
{
    Task<IEnumerable<Ship>> GetShipsAsync();
    Task<Ship> GetShipAsync(int id);
}