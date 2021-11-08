using LightManager.Infrastructure.CQRS.Commands;
using LightManager.Infrastructure.CQRS.Events;
using LightManager.Tests.Utils.Sources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LightManager.Api.Tests.DI
{
    public class ServicesShould
    {
        private IServiceProvider BuildServiceProvider()
        {
            ServiceCollection services = new();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

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
        [GenericTestCaseSource(nameof(CqrsTypes))]
        [GenericTestCaseSource(nameof(ApiTypes))]
        public void Resolve<TService>() where TService : class
        {
            IServiceProvider container = BuildServiceProvider();

            TService service = container.GetRequiredService<TService>();

            Check.That(service).IsNotNull();
        }

        public static IEnumerable<Type> CqrsTypes => new Type[]
        {
            typeof(IEventStore),
            typeof(ICommandDispatcher)
        };

        public static IEnumerable<Type> ApiTypes => new Type[]
        {
            typeof(IConfiguration)
        };
    }
}