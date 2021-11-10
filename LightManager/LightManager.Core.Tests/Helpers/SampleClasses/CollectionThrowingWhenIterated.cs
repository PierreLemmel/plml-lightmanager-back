namespace LightManager.Core.Tests.Helpers.SampleClasses;

public class CollectionThrowingWhenIterated<T> : ICollection<T>
{
    public int Count { get; private set; }
    public bool IsReadOnly => false;

    public CollectionThrowingWhenIterated(int initialCount) => Count = initialCount;

    public void Add(T item) => Count++;
    public void Clear() => Count = 0;
    public bool Contains(T item) => false;
    public void CopyTo(T[] array, int arrayIndex) => Array.Fill(array, default);
    public bool Remove(T item)
    {
        Count--;
        return true;
    }

    public IEnumerator<T> GetEnumerator() => throw new EnumeratedException();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}