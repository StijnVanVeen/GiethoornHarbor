using HarborManagementAPI.Events;

namespace HarborManagementAPI.Repositories;

public interface IEventStoreRepository
{
    Task AddEventAsync(StoreEvent @event);
    Task<IEnumerable<StoreEvent>> GetEventsAsync();
}