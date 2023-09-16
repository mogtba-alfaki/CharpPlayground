using StorageIndexer.Index.HashIndex;
using StorageIndexer.Storage;

namespace StorageIndexer.Indexer; 

public class HashIndexer {
    private readonly IndexedStorageReader _storageReader;
    private readonly IndexedStorageWriter _storageWriter;
    private readonly HashIndex _hashIndex;

    public HashIndexer(IndexedStorageReader storageReader, IndexedStorageWriter storageWriter, HashIndex bTreeIndex) {
        _storageReader = storageReader;
        _storageWriter = storageWriter;
        _hashIndex = bTreeIndex;
    }

    public string Insert(string data) {
        var dataId = HashUtil.GenerateHash(data);
        try {
            var byteOffset = _storageWriter.Append(data);
            _hashIndex.Add(dataId, byteOffset); 
        }
        catch (Exception e) {
            throw e;
        }
        return dataId;
    }

    public string Find(string id) {
        string data; 
        try {
            var byteOffset = _hashIndex.Get(id);
            data = _storageReader.ReadDataFromOffsetAsString(byteOffset); 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw e;
        }
        return data;
    }
}