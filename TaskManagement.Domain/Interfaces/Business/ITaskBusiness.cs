using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Business
{
    public interface ITaskBusiness
    {
        TaskEntity Update(TaskEntity newTaskEntity);

        TaskEntity Create(TaskEntity taskEntity);
    }
}
