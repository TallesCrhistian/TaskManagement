using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Business
{
    public interface ITaskBusiness
    {        
       void Update(TaskEntity newTaskEntity);

        void Create(TaskEntity taskEntity);
    }
}
