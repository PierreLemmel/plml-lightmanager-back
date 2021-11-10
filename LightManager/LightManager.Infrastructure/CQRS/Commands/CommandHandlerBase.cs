namespace LightManager.Infrastructure.CQRS.Commands;

public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : Command
{
    public abstract Task<CommandResult> Handle(TCommand command);

    async Task<CommandResult> ICommandHandler.Handle(Command command)
    {
        TCommand casted = (TCommand)command;
        return await Handle(casted);
    }
}
