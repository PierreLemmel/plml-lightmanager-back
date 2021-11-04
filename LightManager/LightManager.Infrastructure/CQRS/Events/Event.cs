using System;

namespace LightManager.Infrastructure.CQRS.Events
{
    public abstract class Event
    {
        public Guid AggregateId { get; }
        public DateTime Time { get; }
        public string EventType => GetType().Name;

        protected Event(DateTime time, Guid aggregateId)
        {
            Time = time;
            AggregateId = aggregateId;
        }
    }

    public abstract class Event<TData> : Event
    {
        public TData Data { get; }

        protected Event(DateTime time, Guid aggregateId, TData data) : base(time, aggregateId)
        {
            Data = data;
        }
    }
}