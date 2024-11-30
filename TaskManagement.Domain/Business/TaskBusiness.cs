using System.Net;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enumerators;
using TaskManagement.Domain.Interfaces.Business;
using TaskManagement.Utils.Exceptions;
using TaskManagement.Utils.Messages;

namespace TaskManagement.Domain.Business
{
    public class TaskBusiness : ITaskBusiness
    {
        public void Create(TaskEntity taskEntity)
        {
            ValidateIfTaskIsCompleted(taskEntity); 
            
            ValidateIfCreatedAtIsInvalid(taskEntity);
        }

        public void Update(TaskEntity newTaskEntity)
        {
            ValidateIfTaskIsCompleted(newTaskEntity);

            ValidateIfCreatedAtIsInvalid(newTaskEntity);           
        }

        private void ValidateIfTaskIsCompleted(TaskEntity taskEntity)
        {
            if (taskEntity.Status == EnumTaskStatus.Completed)
            {
                if (taskEntity.UpdatedAt is null)
                    throw new CustomException(HttpStatusCode.BadRequest, Messages.PropertyCanBeNull("updatedAt"), new HttpRequestException());

                if (taskEntity.UpdatedAt < taskEntity.CreatedAt)
                    throw new CustomException(HttpStatusCode.BadRequest, Messages.UpdatedAtInvalid, new HttpRequestException());
            }
        }

        private void ValidateIfCreatedAtIsInvalid(TaskEntity taskEntity)
        {
            if (taskEntity.CreatedAt > DateTime.Now)
                throw new CustomException(HttpStatusCode.BadRequest, Messages.CreatedAtInvalid, new HttpRequestException());
        }
    }
}
