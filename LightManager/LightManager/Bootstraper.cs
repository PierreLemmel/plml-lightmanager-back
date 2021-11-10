using LightManager.Infrastructure.CQRS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LightManager.Api;

public static class Bootstraper
{
    public static void InitializeServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LightManager", Version = "v1" });
        });

        IReadOnlyCollection<Type> eventTypes = new List<Type>() { };
        IReadOnlyCollection<Type> commandTypes = new List<Type>() { };

        services.AddCqrs(eventTypes, commandTypes);
        services.AddSingleton(configuration);
    }
}