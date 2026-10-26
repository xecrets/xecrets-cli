#region Copyright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2026 Svante Seleborg, All Rights Reserved.
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

using System.Text;

using Xecrets.Core.Public;

namespace Xecrets.Cli.Run;

/// <summary>
/// A write-only stream that keeps the data in memory as long as it is classified as one of the requested types and
/// does not exceed the size limit for that type. Otherwise all data, including what is already buffered, goes to the
/// file.
/// </summary>
internal sealed class InMemoryOrFileStream(string originalFileName, IFile toFile, IDictionary<InMemoryType, long> inMemory) : Stream
{
    private const int SampleSize = 4096;

    private readonly MemoryStream _bufferStream = new();

    private Stream? _fileStream;

    private bool _isClassified = inMemory.Count == 0;

    private InMemoryType? _type;

    public override bool CanRead => false;

    public override bool CanSeek => false;

    public override bool CanWrite => true;

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    /// <summary>
    /// The type of the data kept in memory, or null if it was written to the file. Set by <see cref="Commit"/>.
    /// </summary>
    public InMemoryType? InMemoryType { get; private set; }

    /// <summary>
    /// The data kept in memory, with text converted to UTF-8. Empty if it was written to the file. Set by <see
    /// cref="Commit"/>, and valid also after the stream is disposed.
    /// </summary>
    public ReadOnlyMemory<byte> Data { get; private set; }

    /// <summary>
    /// Commit the data after all of it has been written successfully, classifying it if that has not already been done,
    /// and writing it to the file if it is not to be kept in memory. The file is created even if there is no data.
    /// </summary>
    public void Commit()
    {
        Classify();

        if (_fileStream == null)
        {
            StartFileWriteIfNotMemory();
        }
        if (_fileStream != null)
        {
            return;
        }

        InMemoryType = _type;
        if (_type != Run.InMemoryType.Text)
        {
            Data = _bufferStream.GetBuffer().AsMemory(0, (int)_bufferStream.Length);
            return;
        }

        _bufferStream.Position = 0;
        using StreamReader reader = new(_bufferStream, new UTF8Encoding(false), detectEncodingFromByteOrderMarks: true, leaveOpen: true);
        Data = Encoding.UTF8.GetBytes(reader.ReadToEnd());
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        Write(buffer.AsSpan(offset, count));
    }

    public override void Write(ReadOnlySpan<byte> buffer)
    {
        if (_fileStream != null)
        {
            _fileStream.Write(buffer);
            return;
        }

        _bufferStream.Write(buffer);

        if (_bufferStream.Length >= SampleSize)
        {
            Classify();
        }

        if (_isClassified)
        {
            StartFileWriteIfNotMemory();
        }
    }

    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        return WriteAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
    }

    public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (_fileStream != null)
        {
            return _fileStream.WriteAsync(buffer, cancellationToken);
        }

        Write(buffer.Span);
        return ValueTask.CompletedTask;
    }

    public override void Flush()
    {
        _fileStream?.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _fileStream?.Dispose();
        }
        base.Dispose(disposing);
    }

    private ReadOnlySpan<byte> Buffer => _bufferStream.GetBuffer().AsSpan(0, (int)_bufferStream.Length);

    private void Classify()
    {
        if (_isClassified)
        {
            return;
        }

        _isClassified = true;

        InMemoryType? categoryType = FileTypeClassifier.Classify(originalFileName, Buffer) switch
        {
            FileCategory.Text => Run.InMemoryType.Text,
            FileCategory.Image => Run.InMemoryType.Image,
            FileCategory.Pdf => Run.InMemoryType.Pdf,
            _ => null,
        };

        if (categoryType != null && inMemory.ContainsKey(categoryType.Value))
        {
            _type = categoryType;
        }
        else if (inMemory.ContainsKey(Run.InMemoryType.Binary))
        {
            _type = Run.InMemoryType.Binary;
        }
    }

    private void StartFileWriteIfNotMemory()
    {
        if (_type != null && _bufferStream.Length <= inMemory[_type.Value])
        {
            return;
        }

        _fileStream = toFile.OpenWrite();
        _fileStream.Write(Buffer);
    }
}
