using TaskManagement.UI.Enumerators;

namespace TaskManagement.UI.Entities
{
    internal class TaskEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public EnumTaskStatus Status { get; set; }
    }
}
