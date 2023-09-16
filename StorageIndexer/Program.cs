using StorageIndexer.Indexer;

try {
    var indexer = IndexerBuilder.BuildHashIndexer();
    var dataId =  indexer.Insert("some data");
    var result = indexer.Find(dataId);
    Console.WriteLine($"Found Result: {result}");
}
catch (Exception e) {
    Console.WriteLine(e);
}
