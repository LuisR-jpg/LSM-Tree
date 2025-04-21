// DataUnit.cs
using System;

// Ideally we should we be able to switch this DataUnit class into more complex
// objects, without changing the whole code
public record DataUnit(long Key, long Data) : IComparable<DataUnit>
{
    public int CompareTo(DataUnit? other)
    {
        return Key.CompareTo(other.Key);
    }
}
