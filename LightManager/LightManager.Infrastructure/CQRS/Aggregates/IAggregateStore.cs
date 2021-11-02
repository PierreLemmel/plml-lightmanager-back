using System;
using System.Collections.Generic;

namespace LightManager.Infrastructure.CQRS.Aggregates
{
    public interface IAggregateStore<TAggregate> where TAggregate : Aggregate
    {
        TAggregate GetById(Guid id);
        void Update(TAggregate aggregate);
    }
}