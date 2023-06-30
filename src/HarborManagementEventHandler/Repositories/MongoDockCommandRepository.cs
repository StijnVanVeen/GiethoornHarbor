using HarborManagementEventHandler.DataAccess;
using HarborManagementEventHandler.Model;
using MongoDB.Driver;

namespace HarborManagementEventHandler.Repositories;

public class MongoDockCommandRepository : IDockCommandRepository
{
    private readonly IHarborContext _context;
    
    public MongoDockCommandRepository(IHarborContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task Insert(Dock dock)
    {
        await _context.Docks.InsertOneAsync(dock);
    }

    public async Task<bool> Update(Dock dock)
    {
        var updateResult = _context
            .Docks
            .ReplaceOneAsync(filter: g => g.Id == dock.Id, replacement: dock);
        
        return updateResult.IsCompletedSuccessfully && updateResult.Result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(int id)
    {
        FilterDefinition<Dock> filter = Builders<Dock>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _context
            .Docks
            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}