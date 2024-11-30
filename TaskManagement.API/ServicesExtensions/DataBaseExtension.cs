using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Context;

namespace TaskManagement.API.ServicesExtensions
{
    public static class DataBaseExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration iConfiguration)
        {
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(iConfiguration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, LogLevel.Information));

            return services;
        }
    }
}
