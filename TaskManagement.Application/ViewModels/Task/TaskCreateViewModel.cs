using TaskManagement.Domain.Enumerators;

namespace TaskManagement.Application.ViewModels.Task
{
    public class TaskCreateViewModel
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public EnumTaskStatus Status { get; set; }
    }
}
