using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightManager.Infrastructure;

public static class InfrastructureDIExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string projectId = configuration.GetSection("Firebase")["ProjectId"];
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "plml-lightmanager-firebase-adminsdk.json");
        services.AddSingleton(FirestoreDb.Create(projectId));
            
        return services;
    }
}