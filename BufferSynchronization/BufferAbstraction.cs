

public class BufferAbstraction {
    private byte[] _buffer;
    private readonly int _maxBufferSize = 4069; 
    private int _bufferSize;
    private double _producerRatio;
    private double _consumerRatio;
    private double _mideanRatio = 55.55; 


    public int BufferSize {
        get => _bufferSize;
        set => _bufferSize = value;
    }

    public  byte[] Buffer {
        get => this._buffer;
        set => _buffer = value;
    }

    public BufferAbstraction(int bufferSize) {
        _bufferSize = bufferSize;
        _buffer = new byte[bufferSize]; 
    }

    public void ReCalculateBufferSize(double producerRatio, double consumerRatio) {
    }

    public void WriteBytes(Stream stream) {
        long bytesLeft = stream.Length;
        int offset = 0;
        var timeTrack = DateTime.Now; 
        while (bytesLeft > 0) {
            if (offset > 0) {
                // tune producer ratio. 
                // find the different between producer ratio and consumer ratio 
                // tune the offset to read from producer to match the consumer ratio 
            }
            if (BufferSize < bytesLeft) {
                throw new ArgumentException("Stream Length Exceeds Buffer Size"); 
            }
            Console.WriteLine($"BufferSize: {_buffer.Length}, StreamLength: ${bytesLeft}");
            int currentBytes = stream.Read(_buffer, offset, (int) bytesLeft); 
            

            if (currentBytes == 0) {
                Console.WriteLine("** Done Reading **");
                break;
            }
            offset += currentBytes;
            bytesLeft -= currentBytes;
        }

        var endTimeTrack = DateTime.Now;
        var timeTook = endTimeTrack.Subtract(timeTrack).TotalMilliseconds;
        _producerRatio =  CalculateRatio(Convert.ToInt32(timeTook), offset);
        LogDebugBuffer();
    }


    public void ReadBytes(Stream stream) {
        int bytesToReadAtATime = 100;
        long bytesLeft = stream.Length; 
        int offset = 0;
        int writtenBytesCount = 0; 
        var trackerStartTime = DateTime.Now;
        while (bytesLeft > 0) {
            stream.Write(_buffer, offset, (int) bytesLeft);
            writtenBytesCount += bytesToReadAtATime; 
        }
        _buffer = ClearBufferFrom(writtenBytesCount); 
        var trackerEndTime = DateTime.Now;
        var timeSpent = trackerStartTime.Subtract(trackerEndTime).TotalMilliseconds;
        var consumerRatio = 
            CalculateRatio(Convert.ToInt32(timeSpent), _buffer.Length);
        _buffer = ClearBufferFrom(offset);
    }

    public double CalculateRatio(int milliseconds, int bytesRead) {
        Console.WriteLine($"ratio calc input: milliseconds: {milliseconds}, bytesRead: {bytesRead}");
        if (bytesRead == 0) {
            return 0; 
        }
        var ratio = Math.Abs(bytesRead/ milliseconds);
        return ratio;
    }
    
    public void ResizeBuffer(int newSize) {
        if (newSize < _buffer.Length) {
            throw new Exception("New buffer Size is smaller than the current buffer"); 
        }

        if (newSize > _maxBufferSize) {
            throw new Exception("Exceeded maximum buffer size"); 
        }
    }
    
    private byte[] ClearBufferFrom(int index) {
        byte[] newBuffer = new byte[_buffer.Length];
        var test = _buffer.Skip(index)
            .TakeLast(index)
            .ToArray();
        newBuffer = test;
        return newBuffer; 
    }
    private void LogDebugBuffer() {
        foreach (var b in _buffer) {
            Console.Write((char) b);
        }
    }
}