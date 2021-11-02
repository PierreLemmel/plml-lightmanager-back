using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightManager.Infrastructure.CQRS.Events
{
    public interface IEventStore
    {
        Task Add(Event @event);
        Task<IReadOnlyCollection<Event>> GetByAggregate(Guid aggregateId);
    }
}