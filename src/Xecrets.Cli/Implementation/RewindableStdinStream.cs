#region Copyright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2024, Svante Seleborg, All Rights Reserved.
 *
 * This code file is part of Xecrets Cli, parts of which in turn are derived from AxCrypt as licensed under GPL v3 or later.
 * 
 * However, this code is not derived from AxCrypt and is separately copyrighted and only licensed as follows unless
 * explicitly licensed otherwise. If you use any part of this code in your software, please see https://www.gnu.org/licenses/
 * for details of what this means for you.
 *
 * Xecrets Cli is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * Xecrets Cli is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with Xecrets Cli.  If not, see <https://www.gnu.org/licenses/>.
 *
 * The source repository can be found at https://github.com/ please go there for more information, suggestions and
 * contributions. You may also visit https://www.axantum.com for more information about the author.
*/

#endregion Copyright and GPL License

namespace Xecrets.Cli.Implementation;

/// <summary>
/// A memory buffering re-openable wrapper around standard input. Since there is only one standard input
/// this may be registered as a singleton and instantiated that way. Except for the rewind buffer,
/// it's still forward only. It cannot be rewound back past a checkpoint. On the other hand if a new
/// instance is instantiated, that also works as way of making a checkpoint.
/// </summary>
/// <remarks>
/// It has a limit to the amount it will buffer in memory, if it is exceeded an exception
/// will be thrown. If this becomes an issue, it should be extended to buffer to disk and
/// include code to definititely securely wipe what it writes to disk.
/// </remarks>
internal class RewindableStdinStream : Stream
{
    private const int MAX_BUFFER = 1024 * 1024;

    private readonly Stream _stdin = Console.OpenStandardInput();

    private class Buffers
    {
        private class Buffer(long position, int length)
        {
            public byte[] Data = new byte[length];

            public long Position = position;
        }

        private readonly List<Buffer> _buffers = [];

        public int Read(long position, byte[] data, int offset, int count)
        {
            foreach (Buffer buffer in _buffers)
            {
                if (buffer.Position <= position && position < buffer.Position + buffer.Data.Length)
                {
                    int bufferOffset = (int)(position - buffer.Position);
                    int maxLengthInBuffer = buffer.Data.Length - bufferOffset;
                    int length = Math.Min(maxLengthInBuffer, count);

                    Array.Copy(buffer.Data, bufferOffset, data, offset, length);
                    return length;
                }
            }
            return 0;
        }

        public void Save(long position, byte[] data, int offset, int length)
        {
            if (position > MAX_BUFFER)
            {
                return;
            }

            Buffer buffer = new Buffer(position, length);
            Array.Copy(data, offset, buffer.Data, 0, length);
            _buffers.Add(buffer);
        }

        public void Clear()
        {
            _buffers.Clear();
        }
    }

    private readonly Buffers _buffers = new();

    private long _position;

    /// <summary>
    /// Causes the virtual position to be repositioned to the last
    /// checkpoint.
    /// </summary>
    private void Rewind()
    {
        _position = 0;
    }

    /// <summary>
    /// Sets a new virtual start position in the stream. Should be called when the consumer knows it won't
    /// rewind it any more.
    /// </summary>
    public void Checkpoint()
    {
        Rewind();
        _buffers.Clear();
    }

    public override bool CanRead => _stdin.CanRead;
 
    public override bool CanSeek => _stdin.CanSeek;

    public override bool CanWrite => _stdin.CanWrite;

    public override long Length => _stdin.Length;
    
    public override long Position { get => _stdin.Position; set => _stdin.Position = value; }

    public override void Flush()
    {
        _stdin.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int length = _buffers.Read(_position, buffer, offset, count);
        if (length == 0)
        {
            length = _stdin.Read(buffer, offset, count);
            _buffers.Save(_position, buffer, offset, length);
        }
        _position += length;
        return length;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _stdin.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _stdin.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _stdin.Write(buffer, offset, count);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Rewind();
        }

        base.Dispose(disposing);
    }
}
