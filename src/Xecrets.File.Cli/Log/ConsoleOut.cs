﻿#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright 2022, Svante Seleborg, All Rights Reserved.
 *
 * This code file is part of Xecrets File Cli, parts of which in turn are derived from AxCrypt as licensed under GPL v3 or later.
 * 
 * However, this code is not derived from AxCrypt and is separately copyrighted and only licensed as follows unless
 * explicitly licensed otherwise. If you use any part of this code in your software, please see https://www.gnu.org/licenses/
 * for details of what this means for you.
 *
 * Xecrets File Cli is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * Xecrets File Cli is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with Xecrets File Cli.  If not, see <https://www.gnu.org/licenses/>.
 *
 * The source repository can be found at https://github.com/ please go there for more information, suggestions and
 * contributions. You may also visit https://www.axantum.com for more information about the author.
*/

#endregion Coypright and GPL License

namespace Xecrets.File.Cli.Log
{
    internal class ConsoleOut
    {
        public bool BlankLinePending { get; set; }

        private readonly TextWriter _writer;

        private bool _newLinePending;

        public ConsoleOut(TextWriter writer)
        {
            _writer = writer;
        }

        public void Write(object text)
        {
            WritePending();
            string m = NormalizeNewLines(text.ToString() ?? string.Empty);
            _writer.Write(m);
            _lastLineLength = 0;
        }

        private static string NormalizeNewLines(string m)
        {
            m = m.Replace("\r\n", Environment.NewLine).Trim(Environment.NewLine.ToCharArray());
            return m;
        }

        public void WriteLine(object text)
        {
            Write(text);
            _writer.WriteLine();
            _lastLineLength = 0;
        }

        private int _lastLineLength = 0;

        public void WriteReturn(object value)
        {
            string m = NormalizeNewLines(value.ToString() ?? string.Empty);
            if (m.Contains(Environment.NewLine))
            {
                throw new InvalidOperationException("Internal error, WriteReturn cannot be called with a string containing new lines.");
            }

            WriteBlankLinePending();

            m = Pad(value);
            _writer.Write(m);

            _lastLineLength = m.Length;
            _writer.Write('\r');
            _newLinePending = true;
        }

        private string Pad(object value)
        {
            var m = value.ToString() ?? string.Empty;
            m = m.PadRight(Math.Max(_lastLineLength, m.Length));
            return m;
        }

        public void WritePending()
        {
            if (_newLinePending)
            {
                _writer.Write(string.Empty.PadRight(_lastLineLength) + '\r');
                _newLinePending = false;
                _lastLineLength = 0;
            }
            WriteBlankLinePending();
        }

        private void WriteBlankLinePending()
        {
            if (BlankLinePending)
            {
                _writer.WriteLine();
                BlankLinePending = false;
                _lastLineLength = 0;
            }
        }
    }
}
