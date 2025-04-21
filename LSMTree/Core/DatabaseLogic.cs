public partial class Database
{
    // TODO: Implement
    private SortedDictionary<string, Dictionary<long, long>> init()
    {
        return new SortedDictionary<string, Dictionary<long, long>>();
    }
    
    private void dump()
    {
        List<DataUnit> list = new List<DataUnit>();
        foreach (var pair in memTable.OrderBy(p => p.Key))
        {
            DataUnit p = new DataUnit(pair.Key, pair.Value);
            list.Add(p);
        }
        SortedSet<KeyOffset> compressedKeys;
        string sstPath;
        string checkpointPath;
        KeyCompressor.CompressDataAndSave(list, Config.LOG_FILE_PATH,
                out compressedKeys, out sstPath, out checkpointPath);
        SSTable newTable = new SSTable(sstPath, compressedKeys);
        ssTables.Add(sstPath, newTable);
        memTable.Clear();
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
