using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskEntity>> List(TaskEntity taskEntity, int pageForIndex);

        Task<int> GetQuantityOfItens(TaskEntity taskEntity);
    }
}
