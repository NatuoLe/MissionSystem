using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public static class ListExtention
{
    public delegate B Getter<in A, out B>(A x);
    public static void Log<T>(this List<T> list)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));
        StringBuilder sb = new StringBuilder();
        foreach (T item in list)
        {
            sb.Append(item.ToString());
            sb.Append('-');
        }
        sb.Remove(sb.Length - 1, 1);
        Debug.Log(sb.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <param name="list"></param>
    /// <param name="ac">(a) => a.b</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Log<A, B>(this List<A> list, Getter<A, B> ac = null)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));
        StringBuilder sb = new StringBuilder();
        if (ac != null)
        {
            foreach (A item in list)
            {
                sb.Append(ac(item).ToString());
                sb.Append('-');
            }
        }
        else
        {
            foreach (A item in list)
            {
                sb.Append(item.ToString());
                sb.Append('-');
            }
        }
        sb.Remove(sb.Length - 1, 1);
        Debug.Log(sb.ToString());
    }

    public static T GetRandomOne<T>(this List<T> list)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T GetRandomOne<T>(this T[] array)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));

        if (list.Count < 2)
            throw new ArgumentException("List count should be at least 2 for a swap.");

        if (firstIndex < 0 || firstIndex >= list.Count)
            throw new ArgumentException("FirstIndex cannot be negative or out of range.");

        if (secondIndex < 0 || secondIndex >= list.Count)
            throw new ArgumentException("SecondIndex cannot be negative or out of range.");

        if (firstIndex == secondIndex) return;

        (list[secondIndex], list[firstIndex]) = (list[firstIndex], list[secondIndex]);
    }

    public static IList<T> Shuffle<T>(this IList<T> self)
    {
        int length = self.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            int s = UnityEngine.Random.Range(0, length) % (i + 1);
            self.Swap(i, s);
        }
        return self;
    }

    public static void RemoveLast<T>(this IList<T> self)
    {
        self.RemoveAt(self.Count - 1);
    }

    public static void AddRangeDistinct<T>(this List<T> list, IEnumerable<T> items) where T : struct
    {
        var existingItems = new HashSet<T>(list);
        foreach (var item in items)
        {
            if (existingItems.Add(item)) // 只有不重复时添加
            {
                list.Add(item);
            }
        }
    }
}
