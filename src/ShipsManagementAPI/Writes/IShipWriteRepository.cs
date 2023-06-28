using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Writes;

public interface IShipWriteRepository
{
    Task<int> Insert(Ship ship);
    Task<int> Update(Ship ship);
    Task<int> Delete(Ship ship);
}