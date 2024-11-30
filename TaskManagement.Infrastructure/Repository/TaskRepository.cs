using Microsoft.EntityFrameworkCore;
using TaskManagement.Utils.CustomMaths;
using TaskManagement.Utils;
using TaskManagement.Infrastructure.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repository;

namespace TaskManagement.Infrastructure.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private AppDbContext _appDbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }


        public async Task<List<TaskEntity>> List(TaskEntity taskEntity, int pageIndex)
        {
            if (taskEntity == null)
            {
                throw new ArgumentNullException(nameof(taskEntity));
            }

            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            IQueryable<TaskEntity> query = _appDbContext.TaskEntities;

            query = this.ApplyFilters(taskEntity, query);

            int itensByPage = ConfigurationHelper.GetItemsPerPage();

            int previousIndex = (int)MathBasic.SubtractNumbers(pageIndex, 1);

            int skipedItens = (int)MathBasic.MultiplyNumbers(previousIndex, itensByPage);

            return await query.Skip(skipedItens).Take(itensByPage).ToListAsync();
        }

        public async Task<int> GetQuantityOfItens(TaskEntity identityTaskEntity)
        {
            if (identityTaskEntity == null)
            {
                throw new ArgumentNullException(nameof(identityTaskEntity));
            }

            IQueryable<TaskEntity> query = _appDbContext.TaskEntities;

            query = this.ApplyFilters(identityTaskEntity, query);

            int quantityOfTaskEntity = await query.CountAsync();

            return quantityOfTaskEntity;
        }

        private IQueryable<TaskEntity> ApplyFilters(TaskEntity filter, IQueryable<TaskEntity> query)
        {           
            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(u => u.Description != null &&
                                         EF.Functions.Like(u.Description, $"%{filter.Description}%"));
            }
            
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(u => u.Title != null &&
                                         EF.Functions.Like(u.Title, $"%{filter.Title}%"));
            }
           
            if (filter.Status != null && filter.Status != 0)
            {
                query = query.Where(u => u.Status == filter.Status);
            }
           
            if (filter.CreatedAt != null && filter.CreatedAt != default)
            {
                query = query.Where(u => u.CreatedAt == filter.CreatedAt);
            }
          
            if (filter.UpdatedAt != null && filter.UpdatedAt != default)
            {
                query = query.Where(u => u.UpdatedAt == filter.UpdatedAt);
            }

            return query;
        }
    }
}
