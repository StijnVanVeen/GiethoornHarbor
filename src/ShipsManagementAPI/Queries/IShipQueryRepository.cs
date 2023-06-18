using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Queries;

public interface IShipQueryRepository
{
    Task<IEnumerable<Ship>> FindAll();
    Task<Ship> FindById(int id);
}