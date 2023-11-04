using System.Collections.Generic;

public static class ArrayUtilities
{
    public static T[] MergeWith<T>(this T[] t1, T[] t2)
    {
        List<T> temp = new();
        foreach (T datum in t1) { temp.Add(datum); }
        foreach (T datum in t2) { temp.Add(datum); }
        return temp.ToArray();
    }

    public static T[] MergeWith<T>(this T[] t1, T t2)
    {
        List<T> temp = new();
        foreach (T datum in t1) { temp.Add(datum); }
        temp.Add(t2);
        return temp.ToArray();
    }
}