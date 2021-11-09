using LightManager.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightManager.Infrastructure.CQRS.Events
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IReadOnlyDictionary<string, IEventHandler> handlersCache;

        public EventDispatcher(IEnumerable<IEventHandler> handlers)
        {
            var pairs = handlers
                .SelectMany(h => h.GetType()
                    .GetGenericVersionsOfInterface(typeof(IEventHandler<>))
                    .Select(i => new KeyValuePair<string, IEventHandler>(
                        i.GenericTypeArguments.Single().Name,
                        h)
                    )
                );

            if (!pairs.Select(kvp => kvp.Key).AreAllDistinct())
                throw new InvalidOperationException("Event types in handlers must be all uniques");

            handlersCache = pairs.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value
            );
        }

        public Task Send(Event @event) => handlersCache
            .TryGetValue(@event.EventType, out IEventHandler? handler) ?
                handler.Handle(@event) :
                throw new InvalidOperationException($"Missing event handler for event type '{@event.EventType}'");
    }
}