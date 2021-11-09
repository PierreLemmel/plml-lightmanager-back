using System.Threading.Tasks;

namespace LightManager.Infrastructure.CQRS.Events
{
    public interface IEventDispatcher
    {
        Task Send(Event @event);
    }
}