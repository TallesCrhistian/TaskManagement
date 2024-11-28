namespace TaskManagement.Domain.Entities
{
    public class TaskEntity : BaseEntity
    {    
        public string Title { get; set; }

        public string? Description { get; set; }      

        public int Status { get; set; }
    }
}
