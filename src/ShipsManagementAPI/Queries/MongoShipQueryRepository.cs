using MongoDB.Bson;
using MongoDB.Driver;
using ShipsManagementAPI.DataAccess;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Queries;

public class MongoShipQueryRepository : IShipQueryRepository
{
    private readonly IShipContext _context;

    public MongoShipQueryRepository(IShipContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<Ship>> FindAll()
    {
        return await _context
            .Ships
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<Ship> FindById(int id)
    {
        return await _context
            .Ships
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }
}