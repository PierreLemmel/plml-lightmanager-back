namespace LightManager;

public static class Collections
{
    public static IReadOnlyCollection<T> Empty<T>() => new List<T>();
}

public static class Collections<T>
{
    public static IReadOnlyCollection<T> Empty => Collections.Empty<T>();
}