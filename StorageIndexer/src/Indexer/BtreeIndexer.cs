using System.Text;
using StorageIndexer.Index.Btree;
using StorageIndexer.Storage;

namespace StorageIndexer.Indexer; 

public class BtreeIndexer {
    private readonly IndexedStorageReader _storageReader;
    private readonly IndexedStorageWriter _storageWriter;
    private readonly BTreeIndex _bTreeIndex; 

    public BtreeIndexer(IndexedStorageReader storageReader, IndexedStorageWriter storageWriter, BTreeIndex bTreeIndex) {
        _storageReader = storageReader;
        _storageWriter = storageWriter;
        _bTreeIndex = bTreeIndex; 
    }

    public string Insert(string data) {
        var dataId = HashUtil.GenerateHash(data);
        try {
             _storageWriter.Append(data);
            _bTreeIndex.Add(data);
        }
        catch (Exception e) {
            throw e;
        }
        return dataId;
    }

    public string Find(string id) {
        string data;  
        try {
            var offset = _bTreeIndex.Get(id);
            data = _storageReader.ReadDataFromOffsetAsString(Convert.ToInt64(offset));    
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw e;
        }
        return data;
    }

    public string PrintIndex() {
        return _bTreeIndex.ToString(); 
    }
}