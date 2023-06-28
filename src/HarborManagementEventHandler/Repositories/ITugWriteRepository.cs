using HarborManagementEventHandler.Model;

namespace HarborManagementEventHandler.Repositories;

public interface ITugWriteRepository
{
    Task Insert(Tugboat tugboat);
    Task<bool> Update(Tugboat tugboat);
    Task<bool> Delete(int id);
}