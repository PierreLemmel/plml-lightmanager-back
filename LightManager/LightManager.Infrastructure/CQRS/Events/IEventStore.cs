using System;
using System.Collections.Generic;

namespace LightManager.Infrastructure.CQRS.Events
{
    public interface IEventStore
    {
        void Add(Event @event);
        IReadOnlyCollection<Event> GetByAggregate(Guid aggregateId);
    }
}