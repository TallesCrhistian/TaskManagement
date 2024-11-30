using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Business;
using TaskManagement.Domain.Interfaces.Business;
using TaskManagement.Domain.Interfaces.Repository;
using TaskManagement.Infrastructure.Repository;

namespace TaskManagement.API.ServicesExtensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskServices, TaskServices>();          

            return services;
        }

        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<ITaskBusiness, TaskBusiness>();

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }

        public static IServiceCollection WorkUnit(this IServiceCollection services)
        {
            services.AddScoped<IWorkUnit, WorkUnit>();

            return services;
        }
    }
}
