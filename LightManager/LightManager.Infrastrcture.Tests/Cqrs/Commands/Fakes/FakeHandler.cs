using LightManager.Infrastructure.CQRS.Commands;
using System;
using System.Threading.Tasks;

namespace LightManager.Infrastructure.Tests.Cqrs.Commands.Fakes
{
    public static class FakeHandler
    {
        public static ICommandHandler<TCommand> Create<TCommand>(Func<TCommand, Task<CommandResult>> handler)
            where TCommand : Command
            => new DelegateHandler<TCommand>(handler);

        public static ICommandHandler<TCommand> Create<TCommand>(Func<TCommand, Task> handler)
            where TCommand : Command
            => new DelegateHandler<TCommand>(async command =>
            {
                await handler(command);
                return CommandResult.Ok();
            });

        public static ICommandHandler<TCommand> Create<TCommand>(Func<Task> handler)
            where TCommand : Command
            => new DelegateHandler<TCommand>(async _ =>
            {
                await handler();
                return CommandResult.Ok();
            });

        public static ICommandHandler<TCommand> Create<TCommand>(Func<TCommand, CommandResult> handler)
            where TCommand : Command
            => new DelegateHandler<TCommand>(command => Task.FromResult(handler(command)));

        public static ICommandHandler<TCommand> Create<TCommand>(Func<CommandResult> handler)
            where TCommand : Command
            => new DelegateHandler<TCommand>(_ => Task.FromResult(handler()));

        public static ICommandHandler<TCommand> Create<TCommand>(Action handler)
            where TCommand : Command
            => new DelegateHandler<TCommand>(_ =>
            {
                handler();
                return Task.FromResult(CommandResult.Ok());
            });

        public static ICommandHandler<TCommand> Empty<TCommand>() where TCommand : Command
            => new DelegateHandler<TCommand>(_ => Task.FromResult(CommandResult.Ok()));

        private class DelegateHandler<TCommand> : CommandHandlerBase<TCommand> where TCommand : Command
        {
            private readonly Func<TCommand, Task<CommandResult>> innerHandler;

            public DelegateHandler(Func<TCommand, Task<CommandResult>> innerHandler) => this.innerHandler = innerHandler;

            public override Task<CommandResult> Handle(TCommand command) => innerHandler(command);
        }
    }
}