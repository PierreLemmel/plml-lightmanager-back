using LightManager.Infrastructure.CQRS.Events;
using LightManager.Infrastructure.Tests.Cqrs.Events.SampleClasses;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Infrastructure.Tests.Cqrs.Events
{
    public class EventDataMappingShould
    {
        [Test]
        [TestCaseSource(nameof(InvalidConstructorInputs))]
        public void Reject_Invalid_Inputs_In_Constructor(IEnumerable<Type> input) => Check
            .ThatCode(() => new EventDataMapping(input))
            .Throws<ArgumentException>();

        [Test]
        [TestCaseSource(nameof(ValidConstructorInputs))]
        public void Accept_Valid_Inputs_In_Constructor(IEnumerable<Type> input) => Check
            .ThatCode(() => new EventDataMapping(input))
            .DoesNotThrow();

        public static IEnumerable<object[]> ValidConstructorInputs => new object[][]
        {
            new object[] { new Type[] { typeof(SomeEvent), typeof(SomeOtherEvent) } },
            new object[] { new Type[] { typeof(SomeEventWithData), typeof(SomeOtherEventWithData) } },
            new object[] { new Type[] { typeof(SomeEvent), typeof(SomeOtherEvent), typeof(SomeEventWithData), typeof(SomeOtherEventWithData) } },
        };

        public static IEnumerable<object[]> InvalidConstructorInputs => new object[][]
        {
            new object[] { new Type[] { typeof(object) } },
            new object[] { new Type[] { typeof(Event) } },
            new object[] { new Type[] { typeof(Event<>) } },
            new object[] { new Type[] { typeof(Event<SomeData>) } },

            new object[] { new Type[] { typeof(SomeEvent), typeof(SomeEventWithData), typeof(object) } },
            new object[] { new Type[] { typeof(SomeEvent), typeof(SomeEventWithData), typeof(Event) } },
            new object[] { new Type[] { typeof(SomeEvent), typeof(SomeEventWithData), typeof(Event<>) } },
            new object[] { new Type[] { typeof(SomeEvent), typeof(SomeEventWithData), typeof(Event<SomeData>) } },
        };
    }
}