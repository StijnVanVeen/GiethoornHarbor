using MongoDB.Driver;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.DataAccess.Mongo;

public class ShipMongoContext : IShipContext
{
    public ShipMongoContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Ships = database.GetCollection<Ship>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        ShipContextSeed.SeedData(Ships);
    }
    
    public IMongoCollection<Ship> Ships { get; }
}