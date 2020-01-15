using FullstackTest.Application;
using FullstackTest.Application.Abstractions.Services;
using FullstackTest.Application.Authentication;
using FullstackTest.Persistence;
using FullstackTest.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FullstackTest.API.Configuration
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(Constant.ConfigSectionApplicationDbContextConnectionString);
            var token = configuration.GetSection(Constant.ConfigSectionTokenManagement).Get<TokenManagement>();

            services.AddSingleton(token);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
