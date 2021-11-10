namespace LightManager.Infrastructure.CQRS.Events;

public interface IEventDataMapping
{
    Event MapEvent(DateTime time, Guid aggregateId, string typeName, string jsonData);
}