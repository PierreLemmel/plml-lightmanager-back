using System;
using System.Threading.Tasks;

namespace LightManager.Infrastructure.CQRS.Commands
{
    public interface ICommandStore
    {
        Task Add(Command command, CommandResult result);
    }
}