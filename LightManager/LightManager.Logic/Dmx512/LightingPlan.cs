namespace LightManager.Logic.Dmx512;


public record StageLightnigPlan(
    string Name,
    IReadOnlyCollection<Fixture> Fixtures
);

public record Fixture(
    string Name,
    int Adress,
    FixtureModelDefinition Model,
    int Mode,

    string? Remark = null
);