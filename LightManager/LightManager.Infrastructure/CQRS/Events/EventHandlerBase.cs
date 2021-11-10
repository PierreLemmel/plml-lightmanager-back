namespace LightManager.Infrastructure.CQRS.Events;

public abstract class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : Event
{
    public abstract Task Handle(TEvent command);

    async Task IEventHandler.Handle(Event command)
    {
        TEvent casted = (TEvent)command;
        await Handle(casted);
    }
}