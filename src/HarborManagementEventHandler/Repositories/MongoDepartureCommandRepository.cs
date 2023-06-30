using HarborManagementEventHandler.DataAccess;
using HarborManagementEventHandler.Model;
using MongoDB.Driver;

namespace HarborManagementEventHandler.Repositories;

public class MongoDepartureCommandRepository : IDepartureCommandRepository
{
    private readonly IHarborContext _context;
    
    public MongoDepartureCommandRepository(IHarborContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task Insert(Departure departure)
    {
        await _context.Departures.InsertOneAsync(departure);
    }

    public async Task<bool> Update(Departure departure)
    {
        var updateResult = _context
            .Departures
            .ReplaceOneAsync(filter: g => g.Id == departure.Id, replacement: departure);
        
        return updateResult.IsCompletedSuccessfully && updateResult.Result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var filter = Builders<Departure>.Filter.Eq(p => p.Id, id);
        
        var deleteResult = await _context
            .Departures
            .DeleteOneAsync(filter);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}