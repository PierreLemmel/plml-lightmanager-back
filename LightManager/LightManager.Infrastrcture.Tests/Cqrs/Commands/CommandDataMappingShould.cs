using LightManager.Infrastructure.CQRS.Commands;
using LightManager.Infrastructure.Tests.Cqrs.Commands.SampleClasses;
using Newtonsoft.Json;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Infrastructure.Tests.Cqrs.Commands
{
    public class CommandDataMappingShould
    {
        [Test]
        [TestCaseSource(nameof(InvalidConstructorInputs))]
        public void Reject_Invalid_Inputs_In_Constructor(IEnumerable<Type> input) => Check
            .ThatCode(() => new CommandDataMapping(input))
            .Throws<ArgumentException>();

        [Test]
        [TestCaseSource(nameof(ValidConstructorInputs))]
        public void Accept_Valid_Inputs_In_Constructor(IEnumerable<Type> input) => Check
            .ThatCode(() => new CommandDataMapping(input))
            .DoesNotThrow();

        [Test]
        [TestCaseSource(nameof(TypeNameNotInMappedTypes))]
        public void Throw_InvalidOperationException_When_TypeName_Is_Not_In_Mapped_Types(
            IEnumerable<Type> mappedTypes,
            string typeName,
            string jsonData)
        {
            CommandDataMapping mapping = new(mappedTypes);

            Check.ThatCode(() => mapping.MapCommand(DateTime.Now, typeName, jsonData))
                .Throws<InvalidOperationException>();
        }

        [Test]
        public void Materializes_Command_Without_Data()
        {
            IEnumerable<Type> mappedTypes = new[] { typeof(SomeCommand), typeof(SomeCommandWithData) };

            CommandDataMapping mapping = new(mappedTypes);

            DateTime time = DateTime.Now;
            Guid aggregateId = Guid.NewGuid();

            Command result = mapping.MapCommand(time, typeof(SomeCommand).Name, "{}");

            Check.That(result).IsInstanceOf<SomeCommand>();
            Check.That(result.Time).IsEqualTo(time);
        }

        [Test]
        public void Materializes_Command_With_Data()
        {
            IEnumerable<Type> mappedTypes = new[] { typeof(SomeCommand), typeof(SomeCommandWithData) };

            CommandDataMapping mapping = new(mappedTypes);

            DateTime time = DateTime.Now;
            Guid aggregateId = Guid.NewGuid();
            SomeData inputData = new(666, "Hello world!");

            Command result = mapping.MapCommand(time, typeof(SomeCommandWithData).Name, JsonConvert.SerializeObject(inputData));

            Check.That(result).IsInstanceOf<SomeCommandWithData>();
            Check.That(result.Time).IsEqualTo(time);

            SomeCommandWithData withData = (SomeCommandWithData)result;
            Check.That(withData.Data).IsEqualTo(inputData);
        }

        public static IEnumerable<object[]> ValidConstructorInputs => new object[][]
        {
            new object[] { new Type[] { typeof(SomeCommand), typeof(SomeOtherCommand) } },
            new object[] { new Type[] { typeof(SomeCommandWithData), typeof(SomeOtherCommandWithData) } },
            new object[] { new Type[] { typeof(SomeCommand), typeof(SomeOtherCommand), typeof(SomeCommandWithData), typeof(SomeOtherCommandWithData) } },
        };

        public static IEnumerable<object[]> InvalidConstructorInputs => new object[][]
        {
            new object[] { new Type[] { typeof(object) } },
            new object[] { new Type[] { typeof(Command) } },
            new object[] { new Type[] { typeof(Command<>) } },
            new object[] { new Type[] { typeof(Command<SomeData>) } },

            new object[] { new Type[] { typeof(SomeCommand), typeof(SomeCommandWithData), typeof(object) } },
            new object[] { new Type[] { typeof(SomeCommand), typeof(SomeCommandWithData), typeof(Command) } },
            new object[] { new Type[] { typeof(SomeCommand), typeof(SomeCommandWithData), typeof(Command<>) } },
            new object[] { new Type[] { typeof(SomeCommand), typeof(SomeCommandWithData), typeof(Command<SomeData>) } },
        };

        public static IEnumerable<object[]> TypeNameNotInMappedTypes => new object[][]
        {
            new object[]
            {
                new Type[]
                {
                    typeof(SomeCommand),
                    typeof(SomeCommandWithData)
                },
                typeof(SomeOtherCommand).Name,
                "{}"
            },
            new object[]
            {
                new Type[]
                {
                    typeof(SomeCommand),
                    typeof(SomeCommandWithData)
                },
                typeof(SomeOtherCommandWithData).Name,
                JsonConvert.SerializeObject(new SomeOtherData(14.0f, true))
            }
        };
    }
}