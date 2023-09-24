using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public abstract class Enumeration
{
    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public string Name { get; private set; }
    public int Id { get; private set; }

    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static int operator +(Enumeration a, int b) => a.Id + b;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static int operator -(Enumeration a, int b) => a.Id - b;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static int operator +(Enumeration a, Enumeration b) => a.Id + b.Id;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static int operator -(Enumeration a, Enumeration b) => a.Id - b.Id;

    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static bool operator ==(Enumeration a, int b) => a.Id == b;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static bool operator !=(Enumeration a, int b) => a.Id != b;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static bool operator ==(Enumeration a, Enumeration b) => a.Id == b.Id && a.Name == b.Name;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static bool operator !=(Enumeration a, Enumeration b) => a.Id != b.Id || a.Name != b.Name;

    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static bool operator <=(Enumeration a, int b) => a.Id <= b;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static bool operator >=(Enumeration a, int b) => a.Id >= b;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static bool operator <=(Enumeration a, Enumeration b) => a.Id <= b.Id;
    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static bool operator >=(Enumeration a, Enumeration b) => a.Id >= b.Id;

    /// <summary>
    /// Matches Id only.
    /// </summary>
    public static implicit operator int(Enumeration a) => a.Id;

    /// <summary>
    /// Matches obj, Id, and Name.
    /// </summary>
    public override bool Equals(object obj) => obj is Enumeration e && Id == e.Id && Name == e.Name;
    public override int GetHashCode() => HashCode.Combine(Id, Name);

    /// <summary>
    /// Return a new instance of the enum matched by obj, Id, and Name.
    /// </summary>
    public static T FindExact<T>(T t) where T : Enumeration, new()
    {
        foreach (var e in ListAll<T>()) if (e.Equals(t)) return e;
        throw new ArgumentOutOfRangeException(t.ToString());
    }

    /// <summary>
    /// Return a new instance of the enum matched by Id.
    /// </summary>
    public static T FindId<T>(int i) where T : Enumeration, new()
    {
        foreach (var e in ListAll<T>()) if (e.Id == i) return e;
        throw new ArgumentOutOfRangeException(i.ToString());
    }

    /// <summary>
    /// Return a new instance of the enum by it's string value: Name.
    /// </summary>
    public static T FindName<T>(string s) where T : Enumeration, new()
    {
        foreach (var e in ListAll<T>()) if (e.Name == s) return e;
        throw new ArgumentOutOfRangeException(s);
    }

    /// <summary>
    /// Return a new instance of the enum matched by Id.
    /// </summary>
    public static T FindMatch<T>(int i, string s) where T : Enumeration, new()
    {
        foreach (var e in ListAll<T>()) if (e.Id == i && e.Name == s) return e;
        throw new ArgumentOutOfRangeException(i.ToString());
    }

    /// <summary>
    /// Get all enums, in order of declaration (not sorted).
    /// </summary>
    public static List<T> ListAll<T>() where T : Enumeration, new() => GetAll<T>().ToList();

    private static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance = new T();

            if (info.GetValue(instance) is T locatedValue)
            {
                yield return locatedValue;
            }
        }
    }
}