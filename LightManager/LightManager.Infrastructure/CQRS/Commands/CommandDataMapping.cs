using LightManager.Reflection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LightManager.Infrastructure.CQRS.Commands
{
    internal class CommandDataMapping : ICommandDataMapping
    {
        private delegate Command CommandFactoryMethod(DateTime time, string jsonData);
        private readonly IReadOnlyDictionary<string, CommandFactoryMethod> mappings;

        public CommandDataMapping(IEnumerable<Type> mappedTypes)
        {
            IReadOnlyCollection<Type> typeCollection = mappedTypes.ToList();

            bool validTypes = typeCollection.All(type => !type.IsAbstract && type.InheritsFrom<Command>());
            if (!validTypes)
                throw new ArgumentException($"Input types must be concrete subtypes of {nameof(Command)}");

            mappings = typeCollection.ToDictionary(
                type => type.Name,
                type => CreateFactoryMethodForType(type)
            );
        }

        private static CommandFactoryMethod CreateFactoryMethodForType(Type CommandType)
        {
            ParameterExpression timeParameter = Expression.Parameter(typeof(DateTime), "time");
            ParameterExpression jsonDataParameter = Expression.Parameter(typeof(string), "jsonData");

            if (CommandType.InheritsFrom(typeof(Command<>)))
            {
                Type dataType = CommandType
                    .GetGenericVersionOfClassInInheritanceTree(typeof(Command<>))
                    .GenericTypeArguments
                    .Single();


                ConstructorInfo? ci = CommandType.GetConstructor(new[] { typeof(DateTime), dataType });
                if (ci is null)
                    throw new InvalidOperationException("Commands with data must provide a constructor with the following arguments: (DateTime time, TData data)");


                MethodInfo deserialize = typeof(JsonConvert)
                    .GetGenericMethodDefinition(nameof(JsonConvert.DeserializeObject))
                    .MakeGenericMethod(dataType);

                Expression body = Expression.New(ci, timeParameter, Expression.Call(deserialize, jsonDataParameter));
                CommandFactoryMethod factory = Expression.Lambda<CommandFactoryMethod>(
                    body,
                    new[] { timeParameter, jsonDataParameter }
                ).Compile();

                return factory;
            }
            else
            {
                ConstructorInfo? ci = CommandType.GetConstructor(new[] { typeof(DateTime) });
                if (ci is null)
                    throw new InvalidOperationException("Commands without data must provide a constructor with the following arguments: (DateTime time)");

                Expression body = Expression.New(ci, timeParameter);
                CommandFactoryMethod factory = Expression.Lambda<CommandFactoryMethod>(
                    body,
                    new[] { timeParameter, jsonDataParameter }
                ).Compile();

                return factory;
            }
        }

        public Command MapCommand(DateTime time, string commandType, string jsonData) => mappings
            .TryGetValue(commandType, out CommandFactoryMethod? factoryMethod) ?
                factoryMethod(time, jsonData) :
                throw new InvalidOperationException($"Impossible to find '{commandType}' in mapped types.");
    }
}