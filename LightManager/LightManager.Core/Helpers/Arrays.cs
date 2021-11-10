namespace LightManager;

public static class Arrays
{
    public static TTo[] Select<TFrom, TTo>(this TFrom[] input, Func<TFrom, TTo> selector)
    {
        TTo[] result = new TTo[input.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = selector(input[i]);
        }

        return result;
    }

    public static void MapTo<TFrom, TTo>(this TFrom[] input, TTo[] output, Func<TFrom, TTo> selector)
    {
        if (output.Length != input.Length)
            throw new InvalidOperationException($"Array length mismatch. Input: {input.Length}. Ouput: {output.Length}");

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = selector(input[i]);
        }
    }

    public static int IndexOf<T>(this T[] array, T item) => Array.IndexOf(array, item);

    public static T[] Merge<T>(IEnumerable<T[]> arrays)
    {
        int capacity = arrays.Sum(arr => arr.Length);
        T[] result = new T[capacity];

        int destIdx = 0;
        foreach (T[] array in arrays)
        {
            int length = array.Length;
            Array.Copy(array, 0, result, destIdx, length);
            destIdx += length;
        }

        return result;
    }

    public static T[] Merge<T>(params T[][] arrays)
    {
        IEnumerable<T[]> asEnumerable = arrays;
        return Merge(asEnumerable);
    }

    public static void EnsureSize<T>(ref T[] array, int size)
    {
        if (array == null || array.Length != size)
            array = new T[size];
    }

    public static T[] ShallowCopy<T>(this T[] array)
    {
        T[] result = new T[array.Length];
        array.CopyTo(result, 0);

        return result;
    }

    public static void CopyTo<T>(this T[] array, T[] other)
    {
        if (other.Length != array.Length)
            throw new InvalidOperationException("Both arrays must be of the same length");

        array.CopyTo(other, 0);
    }

    public static void Set<T>(this T[] array, T value) => array.Set(value, 0, array.Length);
    public static void Set<T>(this T[] array, T value, int startIndex) => array.Set(value, startIndex, array.Length - startIndex);
    public static void Set<T>(this T[] array, T value, int startIndex, int length)
    {
        int lastIndex = length - startIndex;
        for (int i = startIndex; i < lastIndex; i++)
            array[i] = value;
    }

    public static void Set<T>(this T[] array, Func<T> func) => array.Set(func, 0, array.Length);
    public static void Set<T>(this T[] array, Func<T> func, int startIndex) => array.Set(func, startIndex, array.Length - startIndex);
    public static void Set<T>(this T[] array, Func<T> func, int startIndex, int length)
    {
        int lastIndex = length - startIndex;
        for (int i = startIndex; i < lastIndex; i++)
            array[i] = func();
    }

    public static void Set<T>(this T[] array, Func<int, T> func) => array.Set(func, 0, array.Length);
    public static void Set<T>(this T[] array, Func<int, T> func, int startIndex) => array.Set(func, startIndex, array.Length - startIndex);
    public static void Set<T>(this T[] array, Func<int, T> func, int startIndex, int length)
    {
        int lastIndex = length - startIndex;
        for (int i = startIndex; i < lastIndex; i++)
            array[i] = func(i);
    }

    public static void Set<T>(this T[] array, Func<T, T> func) => array.Set(func, 0, array.Length);
    public static void Set<T>(this T[] array, Func<T, T> func, int startIndex) => array.Set(func, startIndex, array.Length - startIndex);
    public static void Set<T>(this T[] array, Func<T, T> func, int startIndex, int length)
    {
        int lastIndex = length - startIndex;
        for (int i = startIndex; i < lastIndex; i++)
            array[i] = func(array[i]);
    }

    public static void ShiftLeft<T>(this T[] array)
    {
        if (array.Length <= 1) return;

        T first = array[0];

        for (int i = 0; i < array.Length - 1; i++)
            array[i] = array[i + 1];

        array[array.Length - 1] = first;
    }

    public static void ShiftRight<T>(this T[] array)
    {
        if (array.Length <= 1) return;

        T last = array[array.Length - 1];

        for (int i = array.Length - 1; i >= 1; i--)
            array[i] = array[i - 1];

        array[0] = last;
    }

    public static T[] Repeated<T>(this T[] array, int count)
    {
        T[] result = new T[array.Length * count];

        for (int i = 0; i < count; i++)
            array.CopyTo(result, i * array.Length);

        return result;
    }

    public static void Reverse<T>(this T[] array)
    {
        int length = array.Length;
        int lastSwap = length / 2;
        for (int i = 0; i < lastSwap; i++)
        {
            Utils.Swap(ref array[i], ref array[length - 1 - i]);
        }
    }

    public static T[] Create<T>(int capacity, Func<T> factoryMethod) => Create(capacity, _ => factoryMethod());
    public static T[] Create<T>(int capacity, Func<int, T> factoryMethod)
    {
        T[] result = new T[capacity];

        for (int i = 0; i < capacity; i++)
            result[i] = factoryMethod(i);

        return result;
    }

    public static T[,] Bidimensionnal<T>(int rows, int cols, Func<int, int, T> factoryMethod)
    {
        T[,] result = new T[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                result[row, col] = factoryMethod(row, col);
            }
        }

        return result;
    }

    public static T[,] Bidimensionnal<T>(int rows, int cols, Func<T> factoryMethod) => Bidimensionnal(rows, cols, (_, _) => factoryMethod());
    public static T[,] Bidimensionnal<T>(int rows, int cols, T value) => Bidimensionnal(rows, cols, (_, _) => value);
}