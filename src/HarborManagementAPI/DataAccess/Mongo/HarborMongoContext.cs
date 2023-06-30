using HarborManagementAPI.Models;
using MongoDB.Driver;

namespace HarborManagementAPI.DataAccess;

public class HarborMongoContext : IHarborContext
{
    public HarborMongoContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Ships = database.GetCollection<Ship>("Ships");
        Docks = database.GetCollection<Dock>("Docks");
        Tugboats = database.GetCollection<Tugboat>("Tugboats");
        Arrivals = database.GetCollection<Arrival>("Arrivals");
        Departures = database.GetCollection<Departure>("Departures");
    }
    
    public IMongoCollection<Ship> Ships { get; }
    public IMongoCollection<Dock> Docks { get; }
    public IMongoCollection<Tugboat> Tugboats { get; }
    public IMongoCollection<Arrival> Arrivals { get; }
    public IMongoCollection<Departure> Departures { get; }
}