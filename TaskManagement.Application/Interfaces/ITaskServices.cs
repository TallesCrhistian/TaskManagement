using TaskManagement.Application.DTOs;
using TaskManagement.Application.ViewModels.Task;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskServices
    {
        Task<ServiceResponseDTO<TaskViewModel>> Create(TaskCreateViewModel taskCreateViewModel);

        Task<ServiceResponseDTO<TaskViewModel>> Read(Guid id);

        Task<ServiceResponseDTO<TaskViewModel>> Update(TaskUpdateViewModel taskUpdateViewModel);

        Task<ServiceResponseDTO<TaskViewModel>> Delete(Guid id);

        Task<ServiceResponseDTO<List<TaskViewModel>>> List(TaskFilterViewModel taskFilterViewModel, int pageForIndex);
    }
}
