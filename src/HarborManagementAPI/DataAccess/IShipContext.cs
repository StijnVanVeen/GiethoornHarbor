using HarborManagementAPI.Models;
using MongoDB.Driver;

namespace HarborManagementAPI.DataAccess;

public interface IShipContext
{
    IMongoCollection<Ship> Ships { get; }
}