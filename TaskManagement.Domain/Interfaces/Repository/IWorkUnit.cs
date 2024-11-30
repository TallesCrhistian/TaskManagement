namespace TaskManagement.Domain.Interfaces.Repository
{
    public interface IWorkUnit
    {
        Task CommitAsync();
        Task DeleteAsync();
        void Rollback();
        Task SaveChangesAsync();
    }
}
