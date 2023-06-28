using HarborManagementAPI.Models;
using MongoDB.Driver;

namespace HarborManagementAPI.DataAccess;

public class ShipContext : IShipContext
{
    public ShipContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Ships = database.GetCollection<Ship>("Ships");
    }
    
    public IMongoCollection<Ship> Ships { get; }
}