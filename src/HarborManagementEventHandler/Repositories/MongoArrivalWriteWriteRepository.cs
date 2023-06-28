using HarborManagementEventHandler.DataAccess;
using HarborManagementEventHandler.Model;
using MongoDB.Driver;

namespace HarborManagementEventHandler.Repositories;

public class MongoArrivalWriteWriteRepository : IArrivalWriteRepository
{
    private readonly IHarborContext _context;
    
    public MongoArrivalWriteWriteRepository(IHarborContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task Insert(Arrival arrival)
    {
        await _context.Arrivals.InsertOneAsync(arrival);
    }

    public async Task<bool> Update(Arrival arrival)
    {
        var updateResult = _context
            .Arrivals
            .ReplaceOneAsync(filter: g => g.Id == arrival.Id, replacement: arrival);
        
        return updateResult.IsCompletedSuccessfully && updateResult.Result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var filter = Builders<Arrival>.Filter.Eq(p => p.Id, id);
        
        var deleteResult = await _context
            .Arrivals
            .DeleteOneAsync(filter);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}