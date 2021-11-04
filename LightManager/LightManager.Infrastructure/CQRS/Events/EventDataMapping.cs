using LightManager.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Infrastructure.CQRS.Events
{
    internal class EventDataMapping : IEventDataMapping
    {
        public EventDataMapping(IEnumerable<Type> mappedTypes)
        {
            IReadOnlyCollection<Type> typeCollection = mappedTypes.ToList();

            bool validTypes = typeCollection.All(type => !type.IsAbstract && type.InheritsFrom<Event>());
            if (!validTypes)
                throw new ArgumentException($"Input types must be concrete subtypes of {nameof(Event)}");
        }

        public Event MapEvent(DateTime time, Guid aggregateId, string typeName, string jsonData)
        {
            throw new NotImplementedException();
        }
    }
}