namespace LightManager.Infrastructure.CQRS.Events
{
    public interface IEventApplyer<TEvent> where TEvent : Event
    {
        void Apply(TEvent @vent);
    }
}