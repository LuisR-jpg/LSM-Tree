using System;
using System.Collections.Generic;

public class Database
{
    private readonly Dictionary<long, long> _storage = new Dictionary<long, long>();

    private List<Tuple<string, Dictionary<long, long>>> sparseIndex = new List<Tuple<string, Dictionary<long, long>>>();

    public void Create(long key, long value)
    {
        if (_storage.ContainsKey(key))
        {
            Console.WriteLine($"Key {key} already exists. Skipping insert.");
            return;
        }

        _storage[key] = value;
    }

    public long? Read(long key)
    {
        if (_storage.TryGetValue(key, out long value))
        {
            Console.WriteLine($"Read: [{key}] = {value}");
            return value;
        }

        Console.WriteLine($"Key {key} not found.");
        return null;
    }
}