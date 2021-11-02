namespace LightManager.Infrastructure.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}