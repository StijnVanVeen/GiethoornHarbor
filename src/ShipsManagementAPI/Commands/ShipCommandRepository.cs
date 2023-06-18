using Microsoft.EntityFrameworkCore;
using ShipsManagementAPI.DataAccess;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.Commands;

public class ShipCommandRepository : IShipCommandRepository
{
    private readonly ShipsManagementDBContext _dbContext;
    
    public ShipCommandRepository(ShipsManagementDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> Insert(Ship ship)
    {
        _dbContext.Add(ship);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> Update(Ship ship)
    {
        try
        {
            _dbContext.Update(ship);
            return await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return 0;
        }
    }

    public async Task<int> Delete(Ship ship)
    {
        try
        {
            _dbContext.Ships.Remove(ship);
            return await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return 0;
        }
    }
}