using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HarborManagementAPI.Repositories;

public class MongoHarborQueryRepository: IHarborQueryRepository
{
    private readonly IHarborContext _context;
    
    public MongoHarborQueryRepository(IHarborContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<Arrival>> GetArrivalsAsync()
    {
        return await _context.Arrivals.Find(a => true).ToListAsync();
    }

    public async Task<Arrival> GetArrivalAsync(int id)
    {
        return await _context.Arrivals.Find(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Departure>> GetDeparturesAsync()
    {
        return await _context.Departures.Find(d => true).ToListAsync();
    }

    public async Task<Departure> GetDepartureAsync(int id)
    {
        return await _context.Departures.Find(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Tugboat>> GetTugsAsync()
    {
        return await _context.Tugboats.Find(t => true).ToListAsync();
    }

    public async Task<IEnumerable<Tugboat>> GetBusyTugs()
    {
        return await _context.Tugboats.Find(t => t.Available == false).ToListAsync();
    }

    public async Task<IEnumerable<Tugboat>> GetFreeTugs()
    {
        return await _context.Tugboats.Find(t => t.Available == true).ToListAsync();
    }

    public async Task<Tugboat> GetTugById(int id)
    {
        return await _context.Tugboats.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Dock>> GetDocksAsync()
    {
        return await _context.Docks.Find(d => true).ToListAsync();
    }

    public async Task<Dock> GetDockAsync(int id)
    {
        return await _context.Docks.Find(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Dock>> GetFreeDocksAsync()
    {
        return await _context.Docks.Find(d => d.Available == true).ToListAsync();
    }

    public async Task<IEnumerable<Dock>> GetBusyDocksAsync()
    {
        return await _context.Docks.Find(d => d.Available == false).ToListAsync();
    }
}