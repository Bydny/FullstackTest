using FullstackTest.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FullstackTest.API.Configuration
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(Constant.ApplicationDbContextConnectionString);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            
            return services;
        }
    }
}
