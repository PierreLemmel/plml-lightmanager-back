namespace LightManager.Reflection;

public static class TypeFilters
{
    public static IEnumerable<Type> Classes(this IEnumerable<Type> types) => types.Where(type => type.IsClass && !type.IsCaptureClass());
    public static IEnumerable<Type> Implementing<TInterface>(this IEnumerable<Type> types) => types.Where(type => type.Implements<TInterface>());
    public static IEnumerable<Type> InheritingFrom<TClass>(this IEnumerable<Type> types, bool canBeEqualToClassType = false) => types.Where(type => type.InheritsFrom<TClass>(canBeEqualToClassType));
    public static IEnumerable<Type> InheritingFrom(this IEnumerable<Type> types, Type classType, bool canBeEqualToClassType = false) => types.Where(type => type.InheritsFrom(classType, canBeEqualToClassType));
    public static IEnumerable<Type> ConcreteTypes(this IEnumerable<Type> types) => types.Classes().Where(type => !type.IsAbstract);
    public static IEnumerable<Type> MakeGenericsFor(this IEnumerable<Type> types, Type openGeneric) => types.Select(type => openGeneric.MakeGenericType(type));
    public static IEnumerable<Type> GenericVersionsOf(this IEnumerable<Type> types, Type openGeneric) => types.Where(type => type.IsGenericVersionOf(openGeneric));
    public static IEnumerable<Type> ExcludeGenericTypeDefinitions(this IEnumerable<Type> types) => types.Where(type => !type.IsGenericTypeDefinition);
    public static IEnumerable<Type> GenericTypeDefinitions(this IEnumerable<Type> types) => types.Where(type => type.IsGenericTypeDefinition);

    public static IEnumerable<Type> MapGenericDefinitions(this IEnumerable<Type> types, IEnumerable<GenericTypeMap> typeMaps)
    {
        Dictionary<Type, IEnumerable<Type>> typeMapCache = typeMaps.ToDictionary(
            typeMap => typeMap.GenericDefinition,
            typeMap => typeMap.Types
        );

        foreach (Type type in types)
        {
            if (!type.IsGenericTypeDefinition)
            {
                yield return type;
            }
            else
            {
                if (typeMapCache.TryGetValue(type, out IEnumerable<Type>? genericParameters))
                {
                    foreach (Type genericParameter in genericParameters)
                    {
                        Type genericType = type.MakeGenericType(genericParameter);
                        yield return genericType;
                    }
                }
            }
        }
    }

    public static IEnumerable<Type> MapGenericDefinitions(this IEnumerable<Type> types, params GenericTypeMap[] typeMaps)
        => types.MapGenericDefinitions(typeMaps.AsEnumerable());
}