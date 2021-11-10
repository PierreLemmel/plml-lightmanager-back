using LightManager.Infrastructure.CQRS.Events;
using LightManager.Infrastructure.Tests.Cqrs.Events.SampleClasses;
using Newtonsoft.Json;

namespace LightManager.Infrastructure.Tests.Cqrs.Events;

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

    [Test]
    [TestCaseSource(nameof(TypeNameNotInMappedTypes))]
    public void Throw_InvalidOperationException_When_TypeName_Is_Not_In_Mapped_Types(
        IEnumerable<Type> mappedTypes,
        string typeName,
        string jsonData)
    {
        EventDataMapping mapping = new(mappedTypes);

        Check.ThatCode(() => mapping.MapEvent(DateTime.Now, Guid.NewGuid(), typeName, jsonData))
            .Throws<InvalidOperationException>();
    }

    [Test]
    public void Materializes_Event_Without_Data()
    {
        IEnumerable<Type> mappedTypes = new[] { typeof(SomeEvent), typeof(SomeEventWithData) };

        EventDataMapping mapping = new(mappedTypes);

        DateTime time = DateTime.Now;
        Guid aggregateId = Guid.NewGuid();

        Event result = mapping.MapEvent(time, aggregateId, typeof(SomeEvent).Name, "{}");

        Check.That(result).IsInstanceOf<SomeEvent>();
        Check.That(result.Time).IsEqualTo(time);
        Check.That(result.AggregateId).IsEqualTo(aggregateId);
    }

    [Test]
    public void Materializes_Event_With_Data()
    {
        IEnumerable<Type> mappedTypes = new[] { typeof(SomeEvent), typeof(SomeEventWithData) };

        EventDataMapping mapping = new(mappedTypes);

        DateTime time = DateTime.Now;
        Guid aggregateId = Guid.NewGuid();
        SomeData inputData = new(666, "Hello world!");

        Event result = mapping.MapEvent(time, aggregateId, typeof(SomeEventWithData).Name, JsonConvert.SerializeObject(inputData));

        Check.That(result).IsInstanceOf<SomeEventWithData>();
        Check.That(result.Time).IsEqualTo(time);
        Check.That(result.AggregateId).IsEqualTo(aggregateId);

        SomeEventWithData withData = (SomeEventWithData)result;
        Check.That(withData.Data).IsEqualTo(inputData);
    }

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

    public static IEnumerable<object[]> TypeNameNotInMappedTypes => new object[][]
    {
            new object[]
            {
                new Type[]
                {
                    typeof(SomeEvent),
                    typeof(SomeEventWithData)
                },
                typeof(SomeOtherEvent).Name,
                "{}"
            },
            new object[]
            {
                new Type[]
                {
                    typeof(SomeEvent),
                    typeof(SomeEventWithData)
                },
                typeof(SomeOtherEventWithData).Name,
                JsonConvert.SerializeObject(new SomeOtherData(14.0f, true))
            }
    };
}
