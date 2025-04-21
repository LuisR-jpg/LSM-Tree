using System;
using System.Collections.Generic;

public partial class Database
{
    private SortedDictionary<string, SSTable> sparseIndex = new SortedDictionary<string, SSTable>();
    private Dictionary<long, long> memTable = new Dictionary<long, long>();

    public Database()
    {
        // lalitor: Make singleton constructor
        /*
            Call init and load data into sparseIndex
        */
    }

    public void Create(long key, long value)
    {
        memTable.Add(key, value);
        if (memTable.Count > Config.MAX_RAM_IN_BYTES) {
            dump();
        }
    }

    public long? Read(long key)
    {
        /*
            Search
        */
        return null;
    }
}
