namespace LightManager.CLI.Firestore;

internal static class FirestoreManagement
{
    private const string PublicCollection = "public";
    private const string Dmx512Document = "dmx512";

    public static async Task PopulatePublicCollection(IConfiguration configuration, IServiceProvider services)
    {
        FirestoreDb firestore = services.GetRequiredService<FirestoreDb>();

        string dmxJsonData = File.ReadAllText("Data/dmx512.json");
        DataFileLayout dmx512Data = JsonConvert.DeserializeObject<DataFileLayout>(dmxJsonData)!;
        
        var dmx512Document = firestore.Document($"{PublicCollection}/{Dmx512Document}");

        await dmx512Document.SetAsync(dmx512Data);
    }
}
