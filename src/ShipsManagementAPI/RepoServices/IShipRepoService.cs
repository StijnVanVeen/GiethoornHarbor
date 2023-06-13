using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.RepoServices;

public interface IShipRepoService
{
    Task<IEnumerable<Ship>> FindAll();
    Task<Ship> FindById(int id);
    Task<int> Insert(Ship ship);
    Task<int> Update(Ship ship);
    Task<int> Delete(Ship ship);
}