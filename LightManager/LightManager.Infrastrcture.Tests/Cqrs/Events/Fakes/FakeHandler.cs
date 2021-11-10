using LightManager.Infrastructure.CQRS.Events;

namespace LightManager.Infrastructure.Tests.Cqrs.Events.Fakes;

public class FakeHandler
{
    public static IEventHandler<TEvent> Create<TEvent>(Func<TEvent, Task> handler)
        where TEvent : Event
        => new DelegateHandler<TEvent>(async @event => await handler(@event));

    public static IEventHandler<TEvent> Create<TEvent>(Func<Task> handler)
        where TEvent : Event
        => new DelegateHandler<TEvent>(async _ => await handler());

    public static IEventHandler<TEvent> Create<TEvent>(Action<TEvent> handler)
        where TEvent : Event
        => new DelegateHandler<TEvent>(@event =>
        {
            handler(@event);
            return Task.CompletedTask;
        });

    public static IEventHandler<TEvent> Create<TEvent>(Action handler)
        where TEvent : Event
        => new DelegateHandler<TEvent>(_ =>
        {
            handler();
            return Task.CompletedTask;
        });

    public static IEventHandler<TEvent> Empty<TEvent>() where TEvent : Event
        => new DelegateHandler<TEvent>(_ => Task.CompletedTask);

    private class DelegateHandler<TEvent> : EventHandlerBase<TEvent> where TEvent : Event
    {
        private readonly Func<TEvent, Task> innerHandler;

        public DelegateHandler(Func<TEvent, Task> innerHandler) => this.innerHandler = innerHandler;

        public override Task Handle(TEvent @event) => innerHandler(@event);
    }
}