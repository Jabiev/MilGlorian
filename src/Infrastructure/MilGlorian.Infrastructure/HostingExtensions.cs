using Microsoft.Extensions.DependencyInjection;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Infrastructure.Services.JWT;

namespace MilGlorian.Infrastructure;

public static class HostingExtensions
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IJWTService, JWTService>();
    }
}
