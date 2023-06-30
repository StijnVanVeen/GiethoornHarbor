using Microsoft.EntityFrameworkCore;
using ShipsManagementAPI.DataAccess;
using ShipsManagementAPI.DataAccess.SQLServer;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Repositories.QueryRepositories;

public class SQLServerShipQueryRepository : IShipQueryRepository
{
    private readonly ShipsManagementDBContext _dbContext;
    
    public SQLServerShipQueryRepository(ShipsManagementDBContext dbContext)
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