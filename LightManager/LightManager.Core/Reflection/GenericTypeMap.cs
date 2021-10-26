using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Reflection
{
    public record GenericTypeMap(Type GenericDefinition, IEnumerable<Type> Types);
}