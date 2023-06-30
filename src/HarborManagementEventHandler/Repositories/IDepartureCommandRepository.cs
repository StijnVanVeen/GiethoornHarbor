using HarborManagementEventHandler.Model;

namespace HarborManagementEventHandler.Repositories;

public interface IDepartureCommandRepository
{
    Task Insert(Departure departure);
    Task<bool> Update(Departure departure);
    Task<bool> Delete(int id);
}