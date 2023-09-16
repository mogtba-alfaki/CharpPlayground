namespace StorageIndexer; 

public static class StoragePaths {
    public static readonly string INDEX_STORAGE_PATH = 
        Path.Combine(Directory.GetCurrentDirectory(), "index.json");
    public static readonly string DATA_STORAGE_PATH = 
        Path.Combine(Directory.GetCurrentDirectory(), "storage.txt"); 
}
