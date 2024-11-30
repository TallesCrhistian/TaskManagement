using Microsoft.OpenApi.Models;

namespace TaskManagement.API.ServicesExtensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {            
                options.SwaggerDoc($"v1", new OpenApiInfo
                {
                    Title = "TaskManagement.Api",
                    Version = $"v1",
                });                          
            });

            services.AddControllersWithViews();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            return services;
        }

    }
}
