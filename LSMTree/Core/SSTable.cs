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

    // Stores pairs of <offset, key>
    public SortedDictionary<long, long> blocksMap = new SortedDictionary<long, long>();

    public long? search(long key)
    {
        int index = -1;
        for(int i = 0; i < this.blocksMap.Count - 1; i++)
        {
            if (key < blocksMap.ElementAt(i + 1).Value)
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            return FileOperations.rangeSearch(
                key,
                this.Path,
                blocksMap.ElementAt(index).Value,
                blocksMap.ElementAt(index + 1).Value
            );
        }
        return null;
    }

    // TODO: Implement
    public static void merge(SSTable tableA, SSTable tableB)
    {
    }
}
