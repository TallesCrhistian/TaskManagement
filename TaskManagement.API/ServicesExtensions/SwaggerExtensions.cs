using Microsoft.OpenApi.Models;
using System.Reflection;

namespace TaskManagement.API.ServicesExtensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

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
