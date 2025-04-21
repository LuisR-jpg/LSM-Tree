using System;
using System.Collections.Generic;

public class SSTable
{
    public SSTable(string _path, SortedSet<KeyOffset> compressedKeys) {
        path = _path;
        foreach (KeyOffset keyOffset in compressedKeys) {
            blocksMap.Add(keyOffset.Id, keyOffset.Offset);
        }
    }


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
            if (key < blocksMap.ElementAt(i + 1).Key)
            {
                index = i;
                break;
            }
        }

        long upper;
        if (index != -1)
        {
            upper = blocksMap.ElementAt(index + 1).Key;
        }
        else
        {
            upper = 1000000; // TODO: FIX
            index = this.blocksMap.Count - 1;
        }
        return FileOperations.rangeSearch(
            key,
            this.Path,
            blocksMap.ElementAt(index).Key,
            upper
        );
    }

    // TODO: Implement
    public static void merge(SSTable tableA, SSTable tableB)
    {
    }
}
