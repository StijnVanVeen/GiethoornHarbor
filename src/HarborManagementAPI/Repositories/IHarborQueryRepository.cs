using HarborManagementAPI.Models;

namespace HarborManagementAPI.Repositories;

public interface IHarborQueryRepository
{
    Task<IEnumerable<Arrival>> GetArrivalsAsync();
    Task<Arrival> GetArrivalAsync(int id);
    
    Task<IEnumerable<Departure>> GetDeparturesAsync();
    Task<Departure> GetDepartureAsync(int id);
    
    Task<IEnumerable<Tugboat>> GetTugsAsync();
    Task<IEnumerable<Tugboat>> GetBusyTugs();
    Task<IEnumerable<Tugboat>> GetFreeTugs();
    Task<Tugboat> GetTugById(int id);
    
    Task<IEnumerable<Dock>> GetDocksAsync();
    Task<Dock> GetDockAsync(int id);
    Task<IEnumerable<Dock>> GetFreeDocksAsync();
    Task<IEnumerable<Dock>> GetBusyDocksAsync();
}