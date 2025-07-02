using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JwtAuth.Application
{
    public static class Startup
    {
        public static IServiceCollection AddMediatrApplication(this IServiceCollection services)
        {
            return services
                .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
