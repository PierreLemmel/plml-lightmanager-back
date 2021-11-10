using LightManager.Infrastructure.CQRS.Commands;

namespace LightManager.Infrastructure.Tests.Cqrs.Commands.SampleClasses;

public class SomeCommand : Command
{
    public SomeCommand(DateTime time) : base(time)
    {
    }
}

public class SomeOtherCommand : Command
{
    public SomeOtherCommand(DateTime time) : base(time)
    {
    }
}

public class SomeCommandWithData : Command<SomeData>
{
    public SomeCommandWithData(DateTime time, SomeData data) : base(time, data)
    {
    }
}

public record SomeData(int Foo, string Bar);

public class SomeOtherCommandWithData : Command<SomeOtherData>
{
    public SomeOtherCommandWithData(DateTime time, SomeOtherData data) : base(time, data)
    {
    }
}

public record SomeOtherData(float Baz, bool Bat);
