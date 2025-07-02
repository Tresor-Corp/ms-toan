using JwtAuth.Core.IRepository;
using JwtAuth.Core.Setting;
using JwtAuth.Infrastructure.Context;
using JwtAuth.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(config.GetConnectionString("Connection")));
            services.AddTransient<DatabaseInitializer>();
            services.Configure<AuthSetting>(config.GetSection("Authentication"));
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer("Bearer", options =>
               {
                   var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!);
                   var issuer = config.GetSection("Authentication:Issuer").Value!;
                   var audience = config.GetSection("Authentication:Audience").Value!;

                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = issuer,
                       ValidAudience = audience,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                   };
               });
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
        public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<DatabaseInitializer>()
            .InitializeAsync();
        }
    }
}
