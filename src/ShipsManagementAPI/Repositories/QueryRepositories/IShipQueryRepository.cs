using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Repositories.QueryRepositories;

public interface IShipQueryRepository
{
    Task<IEnumerable<Ship>> FindAll();
    Task<Ship> FindById(int id);
}