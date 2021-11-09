using LightManager.Infrastructure.CQRS.Commands;
using LightManager.Infrastructure.Tests.Cqrs.Commands.Fakes;
using LightManager.Infrastructure.Tests.Cqrs.Commands.SampleClasses;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightManager.Infrastructure.Tests.Cqrs.Commands
{
    public class CommandDispatcherShould
    {
        [Test]
        [TestCaseSource(nameof(ValidHandlers))]
        public void Create_With_Some_Handlers(IEnumerable<ICommandHandler> handlers) => Check
            .ThatCode(() => new CommandDispatcher(handlers))
            .DoesNotThrow();

        [Test]
        [TestCaseSource(nameof(MultipleHandlersForSameType))]
        public void Throw_Invalid_Operation_Exception_When_Multiple_Handlers_For_Same_Command(IEnumerable<ICommandHandler> handlers) => Check
            .ThatCode(() => new CommandDispatcher(handlers))
            .Throws<InvalidOperationException>();

        [Test]
        public async Task Call_Only_Correct_Handler()
        {
            bool handler1Called = false;
            bool handler2Called = false;
            bool handler3Called = false;
            bool handler4Called = false;

            CommandDispatcher dispatcher = new(new ICommandHandler[]
            {
                FakeHandler.Create<SomeCommand>(() => { handler1Called = true; }),
                FakeHandler.Create<SomeOtherCommand>(() => { handler2Called = true; }),
                FakeHandler.Create<SomeCommandWithData>(() => { handler3Called = true; }),
                FakeHandler.Create<SomeOtherCommandWithData>(() => { handler4Called = true; }),
            });


            await dispatcher.Send(new SomeCommand(DateTime.Now));

            Check.That(handler1Called).IsTrue();
            Check.That(handler2Called).IsFalse();
            Check.That(handler3Called).IsFalse();
            Check.That(handler4Called).IsFalse();
        }

        public static IEnumerable<object[]> ValidHandlers => new object[][]
        {
            new object[]
            {
                new ICommandHandler[]
                {
                    FakeHandler.Empty<SomeCommand>(),
                    FakeHandler.Empty<SomeOtherCommand>(),
                    FakeHandler.Empty<SomeCommandWithData>(),
                    FakeHandler.Empty<SomeOtherCommandWithData>(),
                }
            }
        };

        public static IEnumerable<object[]> MultipleHandlersForSameType => new object[][]
        {
            new object[]
            {
                new ICommandHandler[]
                {
                    FakeHandler.Empty<SomeCommand>(),
                    FakeHandler.Empty<SomeOtherCommand>(),
                    FakeHandler.Empty<SomeCommandWithData>(),
                    FakeHandler.Empty<SomeOtherCommandWithData>(),
                    FakeHandler.Create<SomeCommand>(sc => Task.FromResult(CommandResult.Ok())),
                }
            }
        };
    }
}