using System.Diagnostics.CodeAnalysis;

namespace LightManager;

public static class MoreLinq
{
    public static bool IsEmpty<T>(this IEnumerable<T> sequence) => !sequence.Any();

    public static bool IsSingle<T>(this IEnumerable<T> sequence)
    {
        IEnumerator<T> enumerator = sequence.GetEnumerator();
        return enumerator.MoveNext() && !enumerator.MoveNext();
    }

    public static bool TryGetSingle<T>(this IEnumerable<T> sequence, [MaybeNullWhen(false)] out T value)
    {
        IEnumerator<T> enumerator = sequence.GetEnumerator();

        if (enumerator.MoveNext())
        {
            value = enumerator.Current;
            if (enumerator.MoveNext())
            {
                value = default;
                return false;
            }
            else
                return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    public static bool TryFind<T>(this IEnumerable<T> sequence, Func<T, bool> predicate, [NotNullWhen(true)] out T? value)
    {
        foreach (T elt in sequence)
        {
            if (predicate(elt))
            {
                value = elt!;
                return true;
            }
        }

        value = default;
        return false;
    }

    public static bool TryFind<T>(this IEnumerable<T> sequence, [NotNullWhen(true)] out T? value)
    {
        value = sequence.FirstOrDefault();
        return sequence.Any();
    }

    public static bool AreAllDistinct<T>(this IEnumerable<T> sequence)
    {
        HashSet<T> set = new HashSet<T>();

        foreach (T elt in sequence)
        {
            if (!set.Add(elt))
            {
                return false;
            }
        }

        return true;
    }
    public static bool ContainsDuplicates<T>(this IEnumerable<T> sequence) => !sequence.AreAllDistinct();

    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> sequence) where T : class => sequence
        .Where(t => t != null)
        .Select(t => t!);

    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        foreach (T elt in sequence)
            action(elt);
    }

    public static void ForEach<T, TDiscard>(this IEnumerable<T> sequence, Func<T, TDiscard> action)
    {
        foreach (T elt in sequence)
            _ = action(elt);
    }

    public static IEnumerable<string> Stringify<T>(this IEnumerable<T> sequence) => sequence.Select(elt => elt?.ToString() ?? "");

    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> sequence) => new HashSet<T>(sequence);

    public static IEnumerable<T> Append<T>(this IEnumerable<T> sequence, IEnumerable<T> items) => sequence.Concat(items);
    public static IEnumerable<T> Append<T>(this IEnumerable<T> sequence, params T[] items) => sequence.Concat(items);

    public static IEnumerable<T> WithoutIndex<T>(this IEnumerable<T> sequence, int index) => sequence.Where((_, i) => i != index);
    public static IEnumerable<T> WithoutIndexes<T>(this IEnumerable<T> sequence, IEnumerable<int> indexes) => sequence.Where((_, i) => !indexes.Contains(i));
    public static IEnumerable<T> WithoutIndexes<T>(this IEnumerable<T> sequence, params int[] indexes) => sequence.WithoutIndexes(indexes.AsEnumerable());

    public static IEnumerable<T> ReplaceAtIndex<T>(this IEnumerable<T> sequence, int index, T newValue) => sequence.Select((item, i) => i != index ? item : newValue);
    public static IEnumerable<T> ReplaceAtIndexes<T>(this IEnumerable<T> sequence, IEnumerable<(int index, T newValue)> replaceData)
    {
        IDictionary<int, T> dictionary = new Dictionary<int, T>();

        foreach ((int index, T newValue) in replaceData)
        {
            if (!dictionary.TryAdd(index, newValue))
                throw new InvalidOperationException($"Duplicate entry in replaceData: {index}");
        }

        return sequence.Select((item, i) => dictionary.TryGetValue(i, out T? newValue) ? newValue : item);
    }

    public static IEnumerable<T> ReplaceAtIndexes<T>(this IEnumerable<T> sequence, params (int index, T newValue)[] replaceData) => sequence.ReplaceAtIndexes(replaceData.AsEnumerable());

    public static IEnumerable<T> Reverse<T>(this IEnumerable<T> sequence)
    {
        T[] result = sequence.ToArray();
        result.Reverse();

        return result;
    }

    public static int LastIndex<T>(this IEnumerable<T> sequence) => sequence.Count() - 1;

    public static IEnumerable<T> Except<T>(this IEnumerable<T> sequence, params T[] elts) => sequence.Except(elts.AsEnumerable());

    public static bool None<T>(this IEnumerable<T> sequence, Func<T, bool> predicate) => !sequence.Any(predicate);

    public static T RandomElement<T>(this IEnumerable<T> sequence) => sequence.ToList().RandomElement();
}

public static class Enumerables
{
    public static IEnumerable<T> Merge<T>(IEnumerable<IEnumerable<T>> sequences) => sequences.SelectMany(seq => seq);
    public static IEnumerable<T> Merge<T>(params IEnumerable<T>[] sequences) => Merge(sequences.AsEnumerable());

    public static IEnumerable<T> Single<T>(T elt) => new T[] { elt };

    public static IEnumerable<T> Create<T>(params T[] elts) => elts;

    public static IEnumerable<T> Create<T>(int count, Func<T> selector) => Create(count, _ => selector());
    public static IEnumerable<T> Create<T>(int count, Func<int, T> selector) => Enumerable.Range(0, count).Select(i => selector(i));
}