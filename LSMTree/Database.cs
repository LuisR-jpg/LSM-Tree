using System;
using System.Collections.Generic;

public class Database
{
    private readonly Dictionary<int, int> _storage = new Dictionary<int, int>();

    public void Create(int key, int value)
    {
        if (_storage.ContainsKey(key))
        {
            Console.WriteLine($"Key {key} already exists. Skipping insert.");
            return;
        }

        _storage[key] = value;
        Console.WriteLine($"Inserted: [{key}] = {value}");
    }

    public int? Read(int key)
    {
        if (_storage.TryGetValue(key, out int value))
        {
            Console.WriteLine($"Read: [{key}] = {value}");
            return value;
        }

        Console.WriteLine($"Key {key} not found.");
        return null;
    }
}