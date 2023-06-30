using HarborManagementAPI.Models;

namespace HarborManagementAPI.Repositories;

public interface IShipQueryRepository
{
    Task<IEnumerable<Ship>> GetShipsAsync();
    Task<Ship> GetShipAsync(int id);
}