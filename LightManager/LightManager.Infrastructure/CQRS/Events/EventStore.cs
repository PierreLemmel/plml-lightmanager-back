using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LightManager.Infrastructure.CQRS.Events
{
    internal class EventStore : IEventStore
    {
        private readonly IDbConnection dbConnection;

        public EventStore(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task Add(Event @event)
        {
            EventDataModel dataModel = new(
                Guid.NewGuid(),
                @event.AggregateId,
                @event.Time,
                @event.EventType,
                JsonConvert.SerializeObject(@event)
            );

            string query = $@"INSERT INTO Events(
                                id,
                                aggregateId,
                                time,
                                eventType,
                                data
                            )
                            VALUES(
                                @id,
                                @aggregateId,
					            @time,
					            @eventType,
					            @data
                            )";

            await dbConnection.ExecuteAsync(query, dataModel);
        }

        public async Task<IReadOnlyCollection<Event>> GetByAggregate(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        private record EventDataModel(
            Guid id,
            Guid AggregateId,
            DateTime time,
            string eventType,
            string data
        );
    }
}