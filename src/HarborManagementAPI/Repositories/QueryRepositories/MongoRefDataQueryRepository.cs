using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Models;
using MongoDB.Driver;

namespace HarborManagementAPI.Repositories;

public class MongoRefDataQueryRepository : IShipQueryRepository
{
    private readonly IShipContext _context;

    public MongoRefDataQueryRepository(IShipContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<Ship>> GetShipsAsync()
    {
        return await _context
            .Ships
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<Ship> GetShipAsync(int id)
    {
        return await _context
            .Ships
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }
}