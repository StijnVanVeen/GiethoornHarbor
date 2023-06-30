using HarborManagementEventHandler.Model;

namespace HarborManagementEventHandler.Repositories;

public interface IArrivalCommandRepository
{
    Task Insert(Arrival arrival);
    Task<bool> Update(Arrival arrival);
    Task<bool> Delete(int id);
}