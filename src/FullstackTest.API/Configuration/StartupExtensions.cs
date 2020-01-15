using FullstackTest.Application;
using FullstackTest.Application.Abstractions.Services;
using FullstackTest.Application.Authentication;
using FullstackTest.Persistence;
using FullstackTest.Persistence.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var token = serviceProvider.GetService<TokenManagement>();
                    var secret = Encoding.ASCII.GetBytes(token.Secret);

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = true,
                        ValidIssuer = token.Issuer,
                        ValidateAudience = true,
                        ValidAudience = token.Audience,
                    };
                });

            return services;
        }
    }
}
