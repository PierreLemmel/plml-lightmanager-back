using LightManager.Reflection;

namespace LightManager.Infrastructure.CQRS.Commands;

internal class CommandDispatcher : ICommandDispatcher
{
    private readonly IReadOnlyDictionary<string, ICommandHandler> handlersCache;

    public CommandDispatcher(IEnumerable<ICommandHandler> handlers)
    {
        var pairs = handlers
            .SelectMany(h => h.GetType()
                .GetGenericVersionsOfInterface(typeof(ICommandHandler<>))
                .Select(i => new KeyValuePair<string, ICommandHandler>(
                    i.GenericTypeArguments.Single().Name,
                    h)
                )
            );

        if (!pairs.Select(kvp => kvp.Key).AreAllDistinct())
            throw new InvalidOperationException("Command types in handlers must be all uniques");

        handlersCache = pairs.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value
        );
    }

    public async Task<CommandResult> Send(Command command) => handlersCache
        .TryGetValue(command.CommandType, out ICommandHandler? handler) ?
            await handler.Handle(command) :
            throw new InvalidOperationException($"Missing command handler for command type '{command.CommandType}'");
}
