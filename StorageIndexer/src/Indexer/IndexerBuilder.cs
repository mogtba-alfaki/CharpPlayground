using StorageIndexer.Index;
using StorageIndexer.Index.Btree;
using StorageIndexer.Index.HashIndex;
using StorageIndexer.Storage;

namespace StorageIndexer.Indexer; 

public static class IndexerBuilder {
    private static readonly char SeparationChar = '\n';
    
    
    public static BtreeIndexer BuildBTreeIndexer() {
        IndexedStorageReader reader = new(SeparationChar);
        IndexedStorageWriter writer = new(SeparationChar);
        BTreeIndex bTreeIndex = new(); 
        BtreeIndexer _indexer = new BtreeIndexer(reader, writer,bTreeIndex);
        return _indexer; 
    }

    public static HashIndexer BuildHashIndexer() {
        IndexedStorageReader reader = new(SeparationChar);
        IndexedStorageWriter writer = new(SeparationChar);
        HashIndex index =  new HashIndex();
        HashIndexer _indexer = new HashIndexer(reader, writer, index);
        return _indexer; 
    }
}