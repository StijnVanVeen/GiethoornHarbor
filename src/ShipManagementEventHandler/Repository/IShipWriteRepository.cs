

using ShipManagementEventHandler.Model;

namespace ShipManagementEventHandler.Repository;

public interface IShipWriteRepository
{
    Task<int> Insert(Ship ship);
    Task<int> Update(Ship ship);
    Task<int> Delete(Ship ship);
}