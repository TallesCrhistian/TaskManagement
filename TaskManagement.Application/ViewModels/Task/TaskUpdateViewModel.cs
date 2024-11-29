using TaskManagement.Domain.Enumerators;

namespace TaskManagement.Application.ViewModels.Task
{
    public class TaskUpdateViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public EnumTaskStatus Status { get; set; }      
    }
}
