using System.Text.Json;

namespace StorageIndexer.Index.HashIndex; 

public class HashIndex{
    private  string INDEX_STORAGE_PATH = StoragePaths.INDEX_STORAGE_PATH;
    private Dictionary<string, long> Index;
    private  StreamReader sr; 
    private  StreamWriter sw; 
    public HashIndex() {
        if (!File.Exists(INDEX_STORAGE_PATH)) {
            File.Create(INDEX_STORAGE_PATH); 
        }
        Index =   RestoreFromDisk(); 
    } 
    
    public long Get(string id) {
        long data; 
        try {
         data = Index[id]; 
        }
        catch (Exception e) {
            throw new Exception("No Entry Found"); 
        }

        return data; 
    }

    public void Add(string id, long byteOffset) {
        Index.Add(id, byteOffset);
        Flush(); 
    }

    public void Remove(string id) {
        try {
            var found = Get(id);
            Index.Remove(id); 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
    private Dictionary<string, long> RestoreFromDisk() {
        Dictionary<string, long>? index = null; 
        using (sr = new StreamReader(INDEX_STORAGE_PATH)) {
            var indexString = sr.ReadToEnd();
            Console.WriteLine(indexString);
            if (indexString.Length > 0) {
                index = JsonSerializer.Deserialize<Dictionary<string, long>>(indexString);
            }
        }

        if (index is not null) {
            return index; 
        }
        return new Dictionary<string, long>(); 
    }

    public void RebuildIndex() {
        Index = RestoreFromDisk(); 
    }
    private void Flush() { 
        var serializedIndex = JsonSerializer.Serialize(Index);
        Console.WriteLine(serializedIndex);
        using (sw = new StreamWriter(INDEX_STORAGE_PATH)) {
            sw.Write(serializedIndex);
        }
    }

    public override string ToString() {
        var stringResult = "HashIndex: {\n"; 
        foreach (var key in Index.Keys) {
            stringResult += $"\t{key} : {Index[key]}\n";
        }
        
        stringResult += "}";
        return stringResult; 
    }
}