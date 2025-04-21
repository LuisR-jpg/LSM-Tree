public partial class Database
{
    // TODO: Implement
    private SortedDictionary<string, Dictionary<long, long>> init()
    {
        return new SortedDictionary<string, Dictionary<long, long>>();
    }
    
    // TODO: Implement
    private void dump()
    {
    }

    private long? search(long key)
    {
        long value;
        if (this.memTable.TryGetValue(key, out value))
        {
            return value;
        }

        foreach(KeyValuePair<string, SSTable> refTable in this.ssTables)
        {
            long? response = refTable.Value.search(key);
            if (response != null)
            {
                // Does not return immediately because there might be a newer value
                value = (long)response;
            }
        }
        return value;
    }
}
