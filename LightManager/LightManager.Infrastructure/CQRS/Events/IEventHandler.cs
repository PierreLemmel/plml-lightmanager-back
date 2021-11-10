namespace LightManager.Infrastructure.CQRS.Events;

public interface IEventHandler
{
    Task Handle(Event @event);
}

public interface IEventHandler<TEvent> : IEventHandler where TEvent : Event
{
    Task Handle(TEvent @event);
}