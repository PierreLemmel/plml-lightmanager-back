using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Infrastructure.ReadModels
{
    public abstract record ReadModel(
        string Id,
        string Name,
        DateTime CreationTime,
        DateTime? ModificationTime
    );

    public record UserReadModel(
        string Id,
        string Name,
        DateTime CreationTime,
        DateTime? ModificationTime
    ): ReadModel(Id, Name, CreationTime, ModificationTime);
}