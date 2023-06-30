using HarborManagementEventHandler.DataAccess;
using HarborManagementEventHandler.Model;
using MongoDB.Driver;

namespace HarborManagementEventHandler.Repositories;

public class MongoTugCommandRepository : ITugCommandRepository
{
    private readonly IHarborContext _context;
    
    public MongoTugCommandRepository(IHarborContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task Insert(Tugboat tugboat)
    {
        await _context.Tugboats.InsertOneAsync(tugboat);
    }

    public async Task<bool> Update(Tugboat tugboat)
    {
        var updateResult = _context
            .Tugboats
            .ReplaceOneAsync(filter: g => g.Id == tugboat.Id, replacement: tugboat);
        
        return updateResult.IsCompletedSuccessfully && updateResult.Result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(int id)
    {
        FilterDefinition<Tugboat> filter = Builders<Tugboat>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _context
            .Tugboats
            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}