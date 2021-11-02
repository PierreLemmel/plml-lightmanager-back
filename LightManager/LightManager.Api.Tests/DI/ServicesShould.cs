using GraphQL;
using GraphQL.Types;
using LightManager.Api.Schema;
using LightManager.Api.Tests.DI.Fakes;
using LightManager.Infrastructure.CQRS.Aggregates;
using LightManager.Infrastructure.CQRS.Events;
using LightManager.Tests.Utils.Sources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Api.Tests.DI
{
    public class ServicesShould
    {
        private IServiceProvider BuildServiceProvider()
        {
            ServiceCollection services = new();

            IConfiguration configuration = new FakeConfiguration();
            Bootstraper.InitializeServices(services, configuration);

            IServiceProvider sp = services.BuildServiceProvider();

            return sp;
        }

        [Test]
        public void BootstrapWithoutCrashing()
        {
            IServiceProvider sp = BuildServiceProvider();
            Check.That(sp).IsNotNull();
        }

        [Test]
        [GenericTestCaseSource(nameof(GraphQLSchemaTypes))]
        [GenericTestCaseSource(nameof(GraphQLTypes))]
        [GenericTestCaseSource(nameof(CqrsTypes))]
        public void Resolve<TService>() where TService : class
        {
            IServiceProvider container = BuildServiceProvider();

            TService service = container.GetRequiredService<TService>();

            Check.That(service).IsNotNull();
        }

        public static IEnumerable<Type> CqrsTypes => new Type[]
        {
            typeof(IEventStore),
        };

        public static IEnumerable<Type> GraphQLSchemaTypes => new Type[]
        {
            typeof(LightManagerSchema),

            typeof(LightManagerQuery),
            typeof(LightManagerMutation),
        };

        public static IEnumerable<Type> GraphQLTypes => new Type[]
        {
            typeof(ISchema),
            typeof(IDocumentExecuter),
            typeof(IDocumentWriter)
        };
    }
}