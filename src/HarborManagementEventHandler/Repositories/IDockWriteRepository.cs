using HarborManagementEventHandler.Model;

namespace HarborManagementEventHandler.Repositories;

public interface IDockWriteRepository
{
    Task Insert(Dock dock);
    Task<bool> Update(Dock dock);
    Task<bool> Delete(int id);
}