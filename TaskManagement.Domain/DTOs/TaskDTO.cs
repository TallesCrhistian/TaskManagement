namespace TaskManagement.Domain.DTOs
{
    public class TaskDTO : BaseEntityDTO
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }
    }
}
