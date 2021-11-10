namespace LightManager.Infrastructure.CQRS.Commands;

public interface ICommandHandler
{
    Task<CommandResult> Handle(Command command);
}

public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : Command
{
    Task<CommandResult> Handle(TCommand command);
}
