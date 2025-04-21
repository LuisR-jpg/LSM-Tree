using System;
using System.Collections.Generic;

public class SSTable
{
    private string path = string.Empty;
    public string Path
    {
        get { return path; }
    }

    private string timestamp = string.Empty;
    public string Timestamp
    {
        get { return timestamp; }
    }

    public Dictionary<long, long> blocksMap = new Dictionary<long, long>();

    // TODO: implement
    public long? search(long key)
    {
        return null;
    }

    // TODO: Implement
    public static void merge(SSTable tableA, SSTable tableB)
    {
    }
}