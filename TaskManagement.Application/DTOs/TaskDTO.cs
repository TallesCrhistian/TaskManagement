using TaskManagement.Domain.Enumerators;

namespace TaskManagement.Application.DTOs
{
    public class TaskDTO : BaseEntityDTO
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public EnumTaskStatus Status { get; set; }
    }
}
