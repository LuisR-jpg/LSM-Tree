// KeyOffset.cs
public record KeyOffset(long Id, long Offset) : IComparable<KeyOffset>
{
    public int CompareTo(KeyOffset? other)
    {
        if (other == null) { return 1; }
        return Id.CompareTo(other.Id);
    }
}
