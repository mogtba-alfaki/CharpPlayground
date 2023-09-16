namespace StorageIndexer.Storage;

public class IndexedStorageReader {
    private const FileMode mode = FileMode.OpenOrCreate;
    private const FileAccess access = FileAccess.Read;
    private readonly string StorageSource = StoragePaths.DATA_STORAGE_PATH;
    private FileStream? _fileStream;
    private readonly char SEPERATOR_CHAR;

    public IndexedStorageReader(char separationChar) {
        SEPERATOR_CHAR = separationChar;
        if (!File.Exists(StorageSource)) {
            File.Create(StorageSource); 
        }
    }

    public IEnumerable<byte> ReadDataFromOffset(long byteOffset) => InternalReadFrom(byteOffset);
    public IEnumerable<byte> ReadFirstLine() => InternalReadFrom(0);
    public string ReadAllLines() => InternalReadAllLines(); 

    public string ReadDataFromOffsetAsString(long byteOffset) {
        var bytes = InternalReadFrom(byteOffset);
        return convertBytesToString(bytes); 
    }

    private byte[] InternalReadFrom(long byteOffset) {
        byte[] bytes = {}; 
        
        using (_fileStream = new FileStream(StorageSource, mode, access)) {
            _fileStream.Position = byteOffset;
            do {
                var read = (byte)_fileStream.ReadByte();
                if (read == -1 || (char) read == SEPERATOR_CHAR) {
                    break;
                }

                bytes.Append(read); 
            } while (true);
        }

        return bytes;
    }

    private string InternalReadAllLines() {
        using (_fileStream = new FileStream(StorageSource, mode, access)) {
            var data = new byte[_fileStream.Length];
            int bytesAlreadyRead = 0;
            var bytesToReadCount = (int)_fileStream.Length;
            do {
                var readingPointer = _fileStream
                    .Read(data, bytesAlreadyRead, bytesToReadCount);
                if (readingPointer == 0) {
                    break;
                }

                bytesAlreadyRead += readingPointer;
                bytesToReadCount -= readingPointer;
            } while (bytesAlreadyRead >= bytesToReadCount);

            var result = ""; 
            foreach (var b in data) {
                result += (char) b; 
            }

            return result;
        }
    }
    
    private string convertBytesToString(IEnumerable<byte> bytes) {
        var stringResult = ""; 
        foreach (var b in bytes) {
            stringResult += (char)b; 
        }

        return stringResult; 
    }
    
}