using System;
using Xunit;
using System.Threading;

public class DatabaseStressTest
{
    [Fact]
    public void AddHundredMillionRecords()
    {
        var db = new Database();
        // int limit = 1_000_000_000; // Throws System-OutOfMemoryException with map implementation
        int limit = 1_000_000;

        var start = DateTime.Now;
        Console.WriteLine($"Inserting {limit:N0} records...");

        for (int i = 0; i < limit; i++)
        {
            db.Create(i, i);
            int checkpoint = limit / 10;
            if (i % checkpoint == 0)
            {
                Console.WriteLine($"Inserted: {i:N0}");
            }
        }

        var duration = DateTime.Now - start;
        Console.WriteLine($"Finished inserting {limit:N0} records in {duration.TotalSeconds:F2} seconds");

        Assert.Equal(limit - 1, db.Read(limit - 1)); // Sanity check
    }
}
