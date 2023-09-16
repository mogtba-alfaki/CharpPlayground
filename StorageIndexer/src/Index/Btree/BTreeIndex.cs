
using System.Text.Json;

namespace StorageIndexer.Index.Btree; 

public class BTreeIndex {
    private readonly int INDEX_DEGREE = 2; 
    private  string INDEX_STORAGE_PATH = StoragePaths.INDEX_STORAGE_PATH;
    private  StreamReader sr; 
    private  StreamWriter sw;
    private readonly BTree<string> _btree;  
    public BTreeIndex() {
        if (!File.Exists(INDEX_STORAGE_PATH)) {
            File.Create(INDEX_STORAGE_PATH); 
        }

        _btree = RestoreFromDisk();
    }

    public void Add(string element) {
        _btree.Insert(element);
        Flush();
    }
    
    private void Flush() { 
        var serializedIndex = JsonSerializer.Serialize(_btree.Root);
        Console.WriteLine(serializedIndex);
        using (sw = new StreamWriter(INDEX_STORAGE_PATH)) {
            sw.Write(serializedIndex);
        }
    }
    
    
    private BTree<string> RestoreFromDisk() {
        BTree<string> tree = new BTree<string>(INDEX_DEGREE); 
        using (sr = new StreamReader(INDEX_STORAGE_PATH)) {
            var indexString = sr.ReadToEnd();
            Console.WriteLine(indexString);
            if (indexString.Length > 0) {
                var TreeRootNode  = JsonSerializer.Deserialize<BTreeNode<string>>(indexString);
                tree.Root = TreeRootNode;
            }
        }
        return tree; 
    }

    public  string Get(string element) {
       return _btree.Find(element);
    }

    public override string ToString() {
        return _btree.ToString();
    }
}