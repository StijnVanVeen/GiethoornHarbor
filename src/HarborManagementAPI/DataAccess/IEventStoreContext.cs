using HarborManagementAPI.Events;
using MongoDB.Driver;

namespace HarborManagementAPI.DataAccess;

public interface IEventStoreContext
{
    IMongoCollection<StoreEvent> Events { get; }
}