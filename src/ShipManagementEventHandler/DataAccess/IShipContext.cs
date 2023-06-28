using ShipManagementEventHandler.Model;
using MongoDB.Driver;
using ShipManagementEventHandler.Model;

namespace ShipManagementEventHandler.DataAccess;

public interface IShipContext
{
    IMongoCollection<Ship> Ships { get; }
}