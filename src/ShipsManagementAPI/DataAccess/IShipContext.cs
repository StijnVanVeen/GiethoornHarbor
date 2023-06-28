using MongoDB.Driver;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.DataAccess;

public interface IShipContext
{
    IMongoCollection<Ship> Ships { get; }
}