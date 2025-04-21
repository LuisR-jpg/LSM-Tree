using System;
using System.Collections.Generic;

public sealed partial class Database
{
    private SortedDictionary<string, SSTable> ssTables = new SortedDictionary<string, SSTable>();
    private Dictionary<long, long> memTable = new Dictionary<long, long>();

    private static Database? _database;
    private Database()
    {
        // TODO Call init and load data into sparseIndex
    }

    public static Database GetInstance()
    {
        if (_database == null)
        {
            _database = new Database();
        }
        return _database;
    }

    public void Create(long key, long value)
    {
        /*
            TODO Insert into memtable
            TODO If memtable exceeds MAX_RAM, dump
        */
    }

    public long? Read(long key)
    {
        try
        {
            return this.search(key);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not read key {key}! Exception: {e}");
            return null;
        }
    }
}