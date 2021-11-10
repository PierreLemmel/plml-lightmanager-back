using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace LightManager.Infrastructure.CQRS.Events;

internal class EventStore : IEventStore
{
    private readonly IDbConnection dbConnection;
    private readonly IEventDataMapping eventMapping;

    public EventStore(IDbConnection dbConnection, IEventDataMapping eventMapping)
    {
        this.dbConnection = dbConnection;
        this.eventMapping = eventMapping;
    }

    public async Task Add(Event @event)
    {
        EventDataModel dataModel = new(
            Guid.NewGuid(),
            @event.AggregateId,
            @event.Time,
            @event.EventType,
            JsonConvert.SerializeObject(@event.Data)
        );

        string query = @"INSERT INTO Events(
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
        string query = "SELECT * FROM Events WHERE aggregateId = @aggregateId";

        IEnumerable<EventDataModel> data = await dbConnection.QueryAsync<EventDataModel>(query, new { AggregateId = aggregateId });

        IReadOnlyCollection<Event> result = data
            .Select(dm => eventMapping.MapEvent(dm.Time, dm.AggregateId, dm.EventType, dm.Data))
            .ToList();

        return result;
    }

    private record EventDataModel(
        Guid Id,
        Guid AggregateId,
        DateTime Time,
        string EventType,
        string Data
    );
}
