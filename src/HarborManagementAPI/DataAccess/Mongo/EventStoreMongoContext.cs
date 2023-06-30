using HarborManagementAPI.Events;
using MongoDB.Driver;

namespace HarborManagementAPI.DataAccess;

public class EventStoreMongoContext : IEventStoreContext
{
    public EventStoreMongoContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase("EventStore");

        Events = database.GetCollection<StoreEvent>("Events");
    }
    
    public IMongoCollection<StoreEvent> Events { get;}
}