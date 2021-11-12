using Google.Cloud.Firestore;
using LightManager.CLI.Firestore;
using System.IO;

namespace LightManager.CLI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        IServiceCollection services = new ServiceCollection();
        Bootstrapper.InitializeServices(services, configuration);

        IServiceProvider sp = services.BuildServiceProvider();

        //await EventStoreManagement.CreateSchema(configuration, sp);
        await FirestoreManagement.PopulatePublicCollection(configuration, sp);
    }
}