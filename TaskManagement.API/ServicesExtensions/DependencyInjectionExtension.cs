using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;

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

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {           

            return services;
        }

        public static IServiceCollection WorkUnit(this IServiceCollection services)
        {            

            return services;
        }
    }
}
