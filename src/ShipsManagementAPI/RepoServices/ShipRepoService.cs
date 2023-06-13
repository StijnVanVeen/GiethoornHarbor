using Microsoft.EntityFrameworkCore;
using ShipsManagementAPI.DataAccess;
using ShipsManagementAPI.Model;

namespace ShipsManagementAPI.RepoServices;

public class ShipRepoService : IShipRepoService
{
    private readonly ShipsManagementDBContext _dbContext;

    public ShipRepoService(ShipsManagementDBContext dbContext)
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