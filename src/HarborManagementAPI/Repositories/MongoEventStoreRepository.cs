using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Events;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HarborManagementAPI.Repositories;

public class MongoEventStoreRepository : IEventStoreRepository
{
    private readonly IEventStoreContext _context;
    public MongoEventStoreRepository(IEventStoreContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task AddEventAsync(StoreEvent @event)
    {
        await _context.Events.InsertOneAsync(@event);
    }

    public async Task<IEnumerable<StoreEvent>> GetEventsAsync()
    {
        return await _context.Events.Find(e => true).ToListAsync();
    }
}