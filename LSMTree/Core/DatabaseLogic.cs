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
        sparseIndex.Add(sstPath, newTable);
        memTable.Clear();
    }

    // TODO: Implement
    private KeyValueDataTransferObject search(long key)
    {
        return new KeyValueDataTransferObject();
    }
}
