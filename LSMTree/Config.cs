public static class Config
{
    // public const int MEMTABLE_CAPACITY = 10; // 500 MB
    public const int MEMTABLE_CAPACITY = 10000000; // 500 MB
    public const string LOG_FILE_PATH = "./logs/";
    public const int COMPRESSED_SIZE = 4000; // 4 KB
}
