using LightManager.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightManager.CLI;

public static class Bootstrapper
{
    public static void InitializeServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddSingleton(configuration);
    }
}