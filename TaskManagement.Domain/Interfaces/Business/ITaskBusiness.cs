using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Business
{
    public interface ITaskBusiness
    {        
        Task<TaskEntity> Update(TaskEntity newTaskEntity, TaskEntity oldTaskEntity);

        Task<TaskEntity> Create(TaskEntity taskEntity);
    }
}
