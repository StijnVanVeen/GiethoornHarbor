using MongoDB.Driver;
using ShipsManagementAPI.DataAccess;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Writes;

public class MongoShipWriteRepository : IShipWriteRepository
{
    
    private readonly IShipContext _context;

    public MongoShipWriteRepository(IShipContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<int> Insert(Ship ship)
    {
        await _context.Ships.InsertOneAsync(ship);
        return 1;
    }

    public async Task<int> Update(Ship ship)
    {
        FilterDefinition<Ship> filter = Builders<Ship>.Filter.Eq(p => p.Id, ship.Id);
        
        var updateResult = await _context
            .Ships
            .ReplaceOneAsync(filter, replacement: ship);

        if (updateResult.IsAcknowledged 
            && updateResult.ModifiedCount > 0)
        {
            return 1;
        }
        return 0;
    }

    public async Task<int> Delete(Ship ship)
    {
        FilterDefinition<Ship> filter = Builders<Ship>.Filter.Eq(p => p.Id, ship.Id);

        DeleteResult deleteResult = await _context
            .Ships
            .DeleteOneAsync(filter);

        if (deleteResult.IsAcknowledged 
            && deleteResult.DeletedCount > 0)
        {
            return 1;
        }
        return 0;
    }
}