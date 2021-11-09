using LightManager.Infrastructure.CQRS.Events;
using LightManager.Infrastructure.Tests.Cqrs.Events.Fakes;
using LightManager.Infrastructure.Tests.Cqrs.Events.SampleClasses;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightManager.Infrastructure.Tests.Cqrs.Events
{
    public class EventDispatcherShould
    {
        [Test]
        [TestCaseSource(nameof(ValidHandlers))]
        public void Create_With_Some_Handlers(IEnumerable<IEventHandler> handlers) => Check
            .ThatCode(() => new EventDispatcher(handlers))
            .DoesNotThrow();

        [Test]
        [TestCaseSource(nameof(MultipleHandlersForSameType))]
        public void Throw_Invalid_Operation_Exception_When_Multiple_Handlers_For_Same_Event(IEnumerable<IEventHandler> handlers) => Check
            .ThatCode(() => new EventDispatcher(handlers))
            .Throws<InvalidOperationException>();

        [Test]
        public async Task Call_Only_Correct_Handler()
        {
            bool handler1Called = false;
            bool handler2Called = false;
            bool handler3Called = false;
            bool handler4Called = false;

            EventDispatcher dispatcher = new(new IEventHandler[]
            {
                FakeHandler.Create<SomeEvent>(() => { handler1Called = true; }),
                FakeHandler.Create<SomeOtherEvent>(() => { handler2Called = true; }),
                FakeHandler.Create<SomeEventWithData>(() => { handler3Called = true; }),
                FakeHandler.Create<SomeOtherEventWithData>(() => { handler4Called = true; }),
            });


            await dispatcher.Send(new SomeEvent(DateTime.Now, Guid.NewGuid()));

            Check.That(handler1Called).IsTrue();
            Check.That(handler2Called).IsFalse();
            Check.That(handler3Called).IsFalse();
            Check.That(handler4Called).IsFalse();
        }

        public static IEnumerable<object[]> ValidHandlers => new object[][]
        {
            new object[]
            {
                new IEventHandler[]
                {
                    FakeHandler.Empty<SomeEvent>(),
                    FakeHandler.Empty<SomeOtherEvent>(),
                    FakeHandler.Empty<SomeEventWithData>(),
                    FakeHandler.Empty<SomeOtherEventWithData>(),
                }
            }
        };

        public static IEnumerable<object[]> MultipleHandlersForSameType => new object[][]
        {
            new object[]
            {
                new IEventHandler[]
                {
                    FakeHandler.Empty<SomeEvent>(),
                    FakeHandler.Empty<SomeOtherEvent>(),
                    FakeHandler.Empty<SomeEventWithData>(),
                    FakeHandler.Empty<SomeOtherEventWithData>(),
                    FakeHandler.Empty<SomeEvent>(),
                }
            }
        };
    }
}