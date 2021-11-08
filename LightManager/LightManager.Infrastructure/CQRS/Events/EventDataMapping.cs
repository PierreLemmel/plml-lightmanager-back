using LightManager.Reflection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LightManager.Infrastructure.CQRS.Events
{
    internal class EventDataMapping : IEventDataMapping
    {
        private delegate Event EventFactoryMethod(DateTime time, Guid aggregateId, string jsonData);
        private readonly IReadOnlyDictionary<string, EventFactoryMethod> mappings;

        public EventDataMapping(IEnumerable<Type> mappedTypes)
        {
            IReadOnlyCollection<Type> typeCollection = mappedTypes.ToList();

            bool validTypes = typeCollection.All(type => !type.IsAbstract && type.InheritsFrom<Event>());
            if (!validTypes)
                throw new ArgumentException($"Input types must be concrete subtypes of {nameof(Event)}");

            mappings = typeCollection.ToDictionary(
                type => type.Name,
                type => CreateFactoryMethodForType(type)
            );
        }

        private static EventFactoryMethod CreateFactoryMethodForType(Type eventType)
        {
            ParameterExpression timeParameter = Expression.Parameter(typeof(DateTime), "time");
            ParameterExpression aggregateIdParameter = Expression.Parameter(typeof(Guid), "aggregateId");
            ParameterExpression jsonDataParameter = Expression.Parameter(typeof(string), "jsonData");

            if (eventType.InheritsFrom(typeof(Event<>)))
            {
                Type dataType = eventType
                    .GetGenericVersionOfClassInInheritanceTree(typeof(Event<>))
                    .GenericTypeArguments
                    .Single();

                
                ConstructorInfo? ci = eventType.GetConstructor(new[] { typeof(DateTime), typeof(Guid), dataType });
                if (ci is null)
                    throw new InvalidOperationException("Events with data must provide a constructor with the following arguments: (DateTime time, Guid aggregateId, TData data)");


                MethodInfo deserialize = typeof(JsonConvert)
                    .GetGenericMethodDefinition(nameof(JsonConvert.DeserializeObject))
                    .MakeGenericMethod(dataType);

                Expression body = Expression.New(ci, timeParameter, aggregateIdParameter, Expression.Call(deserialize, jsonDataParameter));
                EventFactoryMethod factory = Expression.Lambda<EventFactoryMethod>(
                    body,
                    new[] { timeParameter, aggregateIdParameter, jsonDataParameter }
                ).Compile();

                return factory;
            }
            else
            {
                ConstructorInfo? ci = eventType.GetConstructor(new[] { typeof(DateTime), typeof(Guid) });
                if (ci is null)
                    throw new InvalidOperationException("Events without data must provide a constructor with the following arguments: (DateTime time, Guid aggregateId)");

                Expression body = Expression.New(ci, timeParameter, aggregateIdParameter);
                EventFactoryMethod factory = Expression.Lambda<EventFactoryMethod>(
                    body,
                    new[] { timeParameter, aggregateIdParameter, jsonDataParameter }
                ).Compile();

                return factory;
            }
        }

        

        public Event MapEvent(DateTime time, Guid aggregateId, string typeName, string jsonData) => mappings
            .TryGetValue(typeName, out EventFactoryMethod? factoryMethod) ?
                factoryMethod(time, aggregateId, jsonData) :
                throw new InvalidOperationException($"Impossible to find '{typeName}' in mapped types.");
    }
}