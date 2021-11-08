﻿using System.Threading.Tasks;

namespace LightManager.Infrastructure.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task<CommandResult> Send(Command command);
    }
}