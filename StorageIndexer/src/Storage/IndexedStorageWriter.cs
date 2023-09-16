namespace StorageIndexer.Storage;
public  class IndexedStorageWriter {
    private FileStream? _fileStream;
    private readonly string StorageSource = StoragePaths.DATA_STORAGE_PATH;
    private const FileMode MODE = FileMode.OpenOrCreate;
    private const FileAccess ACCESS = FileAccess.ReadWrite;
    private readonly char SEPARATION_CHAR;

    public IndexedStorageWriter(char separationChar) {
        SEPARATION_CHAR = separationChar;
        if (!File.Exists(StorageSource)) {
            File.Create(StorageSource); 
        }
    }

    public long writeStringToByteOffset(int byteOffset, string data) => InternalWriteTo(byteOffset, data); 
    
    private long InternalWriteTo(int byteOffset, string data) {
        using (_fileStream = new FileStream(StorageSource, MODE, ACCESS)) {
            var startingOffset = _fileStream.Position;
            Console.WriteLine($"Starting to write at offset: {_fileStream.Position}, data: {data}");
            _fileStream.Position = byteOffset;
            var chars = data.ToCharArray();
            foreach (var c in chars) {
                _fileStream.WriteByte((byte) c);
            }
            _fileStream.WriteByte((byte) SEPARATION_CHAR);
            return startingOffset; 
        }
    }

    public long Append(string data) {
        using (_fileStream = new FileStream(StorageSource, MODE, ACCESS)) {
            var writeByteOffset = _fileStream.Seek(0, SeekOrigin.End); 
            Console.WriteLine($"Starting to write at offset: {writeByteOffset}, data: {data}");
            var chars = data.ToCharArray();
            foreach (var c in chars) {
                _fileStream.WriteByte((byte) c);
            }
            _fileStream.WriteByte((byte) SEPARATION_CHAR);
            return writeByteOffset; 
        }
    }
}