using LightManager.Infrastructure.CQRS.Commands;
using LightManager.Infrastructure.CQRS.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace LightManager.Infrastructure.CQRS
{
    public static class CqrsServicesExtensions
    {
        public static void AddCqrs(this IServiceCollection services, IReadOnlyCollection<Type> eventTypes, IReadOnlyCollection<Type> commandTypes)
        {
            services.AddSingleton<IEventDataMapping>(new EventDataMapping(eventTypes));
            services.AddSingleton<ICommandDataMapping>(new CommandDataMapping(commandTypes));

            services.AddTransient<IEventStore>(sp => new EventStore(
                GetEventStoreConnection(sp),
                sp.GetRequiredService<IEventDataMapping>()
            ));

            services.AddTransient<ICommandStore>(sp => new CommandStore(
                GetEventStoreConnection(sp)
            ));

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
        }

        private static IDbConnection GetEventStoreConnection(IServiceProvider sp)
        {
            IConfiguration configuration = sp.GetRequiredService<IConfiguration>();
            string cs = configuration.GetConnectionString("EventStore");

            NpgsqlConnectionStringBuilder csBuilder = new(cs)
            {
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            using NpgsqlConnection connection = new(csBuilder.ToString());
            connection.Open();

            return connection;
        }
    }
}