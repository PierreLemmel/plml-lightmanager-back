namespace LightManager.Logic.Dmx512;

public record FixtureModelDefinition(
    string Name,
    string Manufacturer,
    string Type,
    ChannelDefinition[] Channels,
    FixtureModeDefinition[] Modes
);

public record ChannelDefinition(string Name, ChannelType Type);

public record FixtureModeDefinition(string Name, IReadOnlyDictionary<int, string> Channels);