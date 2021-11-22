using LightManager.Logic.Dmx512;

namespace LightManager.CLI.Firestore;

[FirestoreData]
public class FixtureModelDataModel
{
    [FirestoreProperty("name")]
    public string Name { get; init; } = "";

    [FirestoreProperty("manufacturer")]
    public string Manufacturer { get; init; } = "";

    [FirestoreProperty("type")]
    public string Type { get; init; } = "";

    [FirestoreProperty("channels")]
    public ChannelDataModel[] Channels { get; init; } = Array.Empty<ChannelDataModel>();

    [FirestoreProperty("modes")]
    public FixtureModeDataModel[] Modes { get; init; } = Array.Empty<FixtureModeDataModel>();
}

[FirestoreData]
public class ChannelDataModel
{
    [FirestoreProperty("name")]
    public string Name { get; init; } = "";

    [FirestoreProperty("type")]
    public string Type { get; init; } = "";
}

[FirestoreData]
public class FixtureModeDataModel
{
    [FirestoreProperty("name")]
    public string Name { get; init; } = "";

    [FirestoreProperty("channels")]
    public Dictionary<string, string> Channels { get; init; } = new();
}

[FirestoreData]
public class DataFileLayout
{
    [FirestoreProperty("fixtureModels")]
    public FixtureModelDataModel[] FixtureModels { get; init; } = Array.Empty<FixtureModelDataModel>();
}