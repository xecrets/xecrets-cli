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

using System.Text;

namespace Xecrets.Cli.Implementation;

internal class AsciiArmorStream(Stream innerStream) : Stream
{
    private StreamWriter? _writer = innerStream.CanWrite ? new StreamWriter(innerStream, Encoding.ASCII) : null;

    private StreamReader? _reader = innerStream.CanRead ? new StreamReader(innerStream, Encoding.ASCII) : null;

    private bool _beginHeaderDone = false;

    private bool _endFooterDone = false;

    private byte[] _buffer = [];

    public override bool CanRead => innerStream.CanRead;

    public override bool CanSeek => false;

    public override bool CanWrite => innerStream.CanWrite;

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    public override void Flush()
    {
        _writer?.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (_endFooterDone)
        {
            return 0;
        }

        while (!_beginHeaderDone)
        {
            string? line = _reader!.ReadLine();
            if (line == null)
            {
                return 0;
            }

            if (line.Contains(Header))
            {
                _beginHeaderDone = true;
            }
        }

        int bytesRead = 0;

        bool TakeFromBuffer()
        {
            int bytesToCopy = Math.Min(_buffer.Length, count);
            if (bytesToCopy == 0)
            {
                return false;
            }

            Array.Copy(_buffer, 0, buffer, offset, bytesToCopy);

            byte[] restOfBuffer = new byte[_buffer.Length - bytesToCopy];
            Array.Copy(_buffer, bytesToCopy, restOfBuffer, 0, restOfBuffer.Length);
            _buffer = restOfBuffer;

            bytesRead += bytesToCopy;
            offset += bytesToCopy;
            count -= bytesToCopy;
            return true;
        }

        while (count > 0)
        {
            if (TakeFromBuffer())
            {
                continue;
            }

            string? base64Data = _reader!.ReadLine();
            if (base64Data == null)
            {
                return bytesRead;
            }
            if (base64Data.Contains(Footer))
            {
                _endFooterDone = true;
                return bytesRead;
            }

            _buffer = Convert.FromBase64String(base64Data);
        }

        return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    const int MaxLineLength = 64;

    const string Header = "-----BEGIN XECRETS ENCRYPTED-----";

    const string Footer = "-----END XECRETS ENCRYPTED-----";

    public override void Write(byte[] buffer, int offset, int count)
    {
        WriteHeader();

        if (_buffer.Length > 0)
        {
            byte[] newBuffer = new byte[_buffer.Length + count];
            Array.Copy(_buffer, newBuffer, _buffer.Length);
            Array.Copy(buffer, offset, newBuffer, _buffer.Length, count);
            buffer = newBuffer;
            offset = 0;
            count = newBuffer.Length;
        }

        int bytesPerLine = MaxLineLength / 4 * 3;
        int lines = count / bytesPerLine;

        for (int n = 0; n < lines; n++)
        {
            string base64Data = Convert.ToBase64String(buffer, offset + n * bytesPerLine, bytesPerLine, Base64FormattingOptions.None);
            _writer!.WriteLine(base64Data);
        }

        int remainingBytes = count % bytesPerLine;
        _buffer = new byte[remainingBytes];
        Array.Copy(buffer, offset + lines * bytesPerLine, _buffer, 0, remainingBytes);
    }

    private void WriteHeader()
    {
        if (!_beginHeaderDone)
        {
            _writer!.WriteLine(Header);
            _beginHeaderDone = true;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && _writer != null)
        {
            WriteFooter();
            Flush();
            _writer.Dispose();
            _writer = null;
        }

        if (disposing && _reader != null)
        {
            _reader.Dispose();
            _reader = null;
        }

        base.Dispose(disposing);
    }

    private void WriteFooter()
    {
        WriteHeader();
        string base64Data = Convert.ToBase64String(_buffer, 0, _buffer.Length, Base64FormattingOptions.None);
        _writer!.WriteLine(base64Data);
        _writer!.WriteLine(Footer);
    }
}
