using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Repositories.CommandRepositories;

public interface IShipCommandRepository
{
    Task<int> Insert(Ship ship);
    Task<int> Update(Ship ship);
    Task<int> Delete(Ship ship);
}