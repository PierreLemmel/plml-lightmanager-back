using System;
using System.Collections.Generic;
using System.Linq;
using LightManager.Infrastructure.CQRS.Events;

namespace LightManager.Infrastructure.CQRS.Aggregates
{
    public abstract class Aggregate
    {
        private readonly ICollection<Event> changes = new List<Event>();

        public Guid Id { get; internal set; }
        public int Version { get; private set; }
        public DateTime CreationTime { get; internal set; }
        public DateTime ModificationTime { get; internal set; }
        public DateTime? DeletionTime { get; internal set; }
        public bool Deleted => DeletionTime is not null;

        public string AggregateType => GetType().Name;

        internal IEnumerable<Event> GetUncommittedChanges() => changes;
        internal void MarkChangesAsCommitted() => changes.Clear();

        internal void LoadHistory(IEnumerable<Event> history)
        {
            foreach (Event e in history)
            {
                ApplyChange(e, false);
                Version++;
            }
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            dynamic asDynamic = this;
            asDynamic.Apply(@event);

            if (isNew)
                changes.Add(@event);
        }
    }

    public abstract class Aggregate<TState> where TState : new()
    {
        public TState State { get; protected internal set; } = new();
    }
}