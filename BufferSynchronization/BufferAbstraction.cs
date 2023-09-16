

public class BufferAbstraction {
    private byte[] _buffer;
    private readonly int _maxBufferSize = 4069; 
    private int _bufferSize;
    private double _producerRatio;
    private double _consumerRatio;
    private double MidealRatio = 55.55; 

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
            /*
             * this method of reading from stream does not fulfil our case
             * we want to be able to read a resizable chunk from the stream which
             * will be defined by the sync calculations 
             */
            int currentBytes = stream.Read(_buffer, offset, (int)bytesLeft); 
            if (currentBytes == 0) {
                Console.WriteLine("** Done Reading **");
                break;
            }
            offset += currentBytes;
            bytesLeft -= currentBytes;
        }

        var endTimeTrack = DateTime.Now;
        var timeTook = endTimeTrack.Subtract(timeTrack).TotalMilliseconds;
        var producerRatio = CalculateRatio(Convert.ToInt32(timeTook), offset);

        Console.WriteLine($"************ Buffer Content: {_buffer.Length} ************");
        foreach (var b in _buffer) {
            Console.Write(b);
        }
    }

    public double CalculateRatio(int milliseconds, int bytesRead) {
        if (bytesRead == 0) {
            return 0; 
        }
        _producerRatio = Math.Abs(milliseconds / bytesRead);
        return _producerRatio;
    }
    
    public void ResizeBuffer(int newSize) {
        if (newSize < _buffer.Length) {
            throw new Exception("New buffer Size is smaller than the current buffer"); 
        }

        if (newSize > _maxBufferSize) {
            throw new Exception("Exceeded maximum buffer size"); 
        }
    }
}