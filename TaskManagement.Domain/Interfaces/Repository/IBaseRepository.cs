using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repository
{
    public interface IBaseRepository
    {
        Task<TEntity> Create<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<TEntity> Delete<TEntity>(Guid idEntity) where TEntity : BaseEntity;
        Task<TEntity> Read<TEntity>(Guid idEntity) where TEntity : BaseEntity;
        Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}
