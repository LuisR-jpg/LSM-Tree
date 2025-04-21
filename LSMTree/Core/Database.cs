using System;
using System.Collections.Generic;

public partial class Database
{
    private SortedDictionary<string, Dictionary<long, long>> sparseIndex = new SortedDictionary<string, Dictionary<long, long>>();
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
        /*
            Insert into memtable
            If memtable exceeds MAX_RAM, dump
        */
    }

    public long? Read(long key)
    {
        /*
            Search
        */
        return null;
    }
}