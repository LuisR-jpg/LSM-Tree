using System;
using System.Collections.Generic;

private class SSTable
{
  private string path;
  public string Path
  {
    get { return path; }
  }

  private string timestamp;
  public string Timestamp
  {
    get { return timestamp; }
  }

  public Dictionary<long, long> blocksMap = new Dictionary<long, long>();

  static public merge(SSTable tableA, SSTable tableB)
  {
  }

  public long? search(long key)
  {
  }
}