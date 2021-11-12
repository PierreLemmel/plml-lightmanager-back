using LightManager.Logic.Dmx512;

namespace LightManager.CLI.Firestore;

[FirestoreData]
public class FixtureModelDataModel
{
    [FirestoreProperty]
    public string Name { get; init; } = "";

    [FirestoreProperty]
    public string Manufacturer { get; init; } = "";

    [FirestoreProperty]
    public string Type { get; init; } = "";

    [FirestoreProperty]
    public ChannelDataModel[] Channels { get; init; } = Array.Empty<ChannelDataModel>();

    [FirestoreProperty]
    public FixtureModeDataModel[] Modes { get; init; } = Array.Empty<FixtureModeDataModel>();
}

[FirestoreData]
public class ChannelDataModel
{
    [FirestoreProperty]
    public string Name { get; init; } = "";

    [FirestoreProperty]
    public ChannelType Type { get; init; }
}

[FirestoreData]
public class FixtureModeDataModel
{
    [FirestoreProperty]
    public string Name { get; init; } = "";

    [FirestoreProperty]
    public Dictionary<string, string> Channels { get; init; } = new();
}

[FirestoreData]
public class DataFileLayout
{
    [FirestoreProperty]
    public FixtureModelDataModel[] FixtureModels { get; init; } = Array.Empty<FixtureModelDataModel>();
}