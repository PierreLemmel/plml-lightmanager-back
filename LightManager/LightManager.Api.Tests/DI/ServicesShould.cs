using LightManager.Api.Schema;
using LightManager.Tests.Utils.Sources;
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

            Bootstraper.InitializeServices(services);

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
        [GenericTestCaseSource(nameof(GraphQLTypes))]
        public void Resolve<TService>() where TService : class
        {
            IServiceProvider container = BuildServiceProvider();

            TService service = container.GetRequiredService<TService>();

            Check.That(service).IsNotNull();
        }

        public static IEnumerable<Type> GraphQLTypes => new Type[]
        {
            typeof(LightManagerSchema),
            typeof(LightManagerQuery),
            typeof(LightManagerMutation),
        };
    }
}