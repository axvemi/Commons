using System;
using System.Collections.Generic;

namespace Axvemi.Commons;

public static class Lists
{
    /// <summary>
    /// Shuffles a list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        Random random = new();
        int n = list.Count;
        for (int i = 0; i < (n - 1); i++)
        {
            int r = i + random.Next(n - i);
            (list[r], list[i]) = (list[i], list[r]);
        }
    }
}