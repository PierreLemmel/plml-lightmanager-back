using LightManager.Infrastructure.CQRS.Contracts;
using System;

namespace LightManager.Infrastructure.CQRS.Events
{
    public abstract class Event : IData
    {
        public Guid AggregateId { get; }
        public DateTime Time { get; }
        public string EventType => GetType().Name;

        protected virtual object GetData() => new object();
        public object Data => GetData();

        protected Event(DateTime time, Guid aggregateId)
        {
            Time = time;
            AggregateId = aggregateId;
        }
    }

    public abstract class Event<TData> : Event, IData<TData>
        where TData : notnull
    {
        public new TData Data { get; }

        protected override sealed object GetData() => Data;

        protected Event(DateTime time, Guid aggregateId, TData data) : base(time, aggregateId)
        {
            Data = data;
        }
    }
}