using Microsoft.EntityFrameworkCore;
using ShipsManagementAPI.DataAccess;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Queries;

public class ShipQueryRepository : IShipQueryRepository
{
    private readonly ShipsManagementDBContext _dbContext;
    
    public ShipQueryRepository(ShipsManagementDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Ship>> FindAll()
    {
        return await _dbContext.Ships.ToListAsync();
    }

    public async Task<Ship> FindById(int id)
    {
        return await _dbContext.Ships.FirstOrDefaultAsync(s => s.Id == id);
    }
}