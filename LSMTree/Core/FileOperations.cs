using System;
using System.Collections.Generic;

static public class FileOperations
{
    public static long? rangeSearch(long key, string path, long left_bound, long right_bound)
    {
        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (var reader = new StreamReader(fs))
        {
            fs.Seek(left_bound, SeekOrigin.Begin);

            while (fs.Position < right_bound && !reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (line == null) break;

                // Parse line: assume format "key,value"
                var parts = line.Split(',');
                if (parts.Length != 2)
                {
                    throw new FormatException($"Invalid line format: '{line}'. Expected format: key,value");
                }

                if (long.TryParse(parts[0], out long currentKey) && long.TryParse(parts[1], out long currentValue))
                {
                    if (currentKey == key)
                    {
                        return currentValue;
                    }
                }
            }
        }

        return null;
    }
}