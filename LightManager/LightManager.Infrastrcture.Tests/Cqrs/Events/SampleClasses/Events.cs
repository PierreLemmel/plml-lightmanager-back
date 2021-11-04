using LightManager.Infrastructure.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Infrastructure.Tests.Cqrs.Events.SampleClasses
{
    public class SomeEvent : Event
    {
        public SomeEvent(DateTime time, Guid aggregateId) : base(time, aggregateId)
        {
        }
    }

    public class SomeOtherEvent : Event
    {
        public SomeOtherEvent(DateTime time, Guid aggregateId) : base(time, aggregateId)
        {
        }
    }

    public class SomeEventWithData : Event<SomeData>
    {
        public SomeEventWithData(DateTime time, Guid aggregateId, SomeData data) : base(time, aggregateId, data)
        {
        }
    }

    public record SomeData(int Foo, string Bar);

    public class SomeOtherEventWithData : Event<SomeOtherData>
    {
        public SomeOtherEventWithData(DateTime time, Guid aggregateId, SomeOtherData data) : base(time, aggregateId, data)
        {
        }
    }

    public record SomeOtherData(float Baz, bool Bat);
}