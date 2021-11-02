using System.Threading.Tasks;

namespace LightManager.Infrastructure.CQRS.Commands
{
    public interface ICommandHandler { }
    
    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}