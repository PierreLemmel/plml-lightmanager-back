using System;

namespace LightManager.Infrastructure.CQRS.Commands
{
    public interface ICommandDataMapping
    {
        Command MapCommand(DateTime time, string commandType, string jsonData);
    }
}