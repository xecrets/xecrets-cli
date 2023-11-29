#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright © 2022-2023, Svante Seleborg, All Rights Reserved.
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

using System.Text.Json;

using AxCrypt.Core.UI;

using Xecrets.File.Cli.Implementation;
using Xecrets.File.Cli.Public;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.File.Cli.Log
{
    internal class JsonLogger(TotalsTracker totalsTracker, bool progress) : ILogger
    {
        public IProgressContext Progress { get; } = new TotalsProgressContext(progress ? new JsonProgressContext(totalsTracker) : new NoProgressContext(), totalsTracker);

        public void Log(Status status)
        {
            Log(status.OpCode, status);
        }

        public void Log(XfOpCode opCode, Status status)
        {
            FlushPending();

            if (opCode == XfOpCode.CliTextMessage && status.Message.Length == 0)
            {
                return;
            }

            var statusMessage = new CliMessage()
            {
                OpCode = (int)opCode,
                OpCodeName = opCode.ToString(),
                Message = status.Message.Replace("\r\n", Environment.NewLine),
                From = ToNullIfEmpty(status.From),
                Email = ToNullIfEmpty(status.Email),
                File = ToNullIfEmpty(status.File),
                To = ToNullIfEmpty(status.To),
                CliApiVersion = ToNullIfEmpty(status.CliVersion),
                ProgramVersion = ToNullIfEmpty(status.ProgramVersion),
                Platform = ToNullIfEmpty(status.Platform),
                OriginalFileName = ToNullIfEmpty(status.OriginalFileName),
                Result = ToNullIfEmpty(status.Result),
                Status = (int)status.StatusCode,
                StatusName = status.StatusCode != 0 ? status.StatusCode.ToString() : null,
                Id = status.Id.Length > 0 ? status.Id : null,
                Utc = status.Utc,
            };

            JsonConsoleOut(statusMessage);
        }

        private static void JsonConsoleOut(CliMessage jsonMessage)
        {
            var json = JsonSerializer.Serialize(jsonMessage, typeof(CliMessage), SourceGenerationContext.Default);
            New<ConsoleOut>().WriteLine(json);
        }

        public void Log(string message)
        {
            Log(XfOpCode.CliTextMessage, new Status(message));
        }

        public void FlushPending()
        {
            New<Splash>().Write(m => JsonConsoleOut(new CliMessage() { OpCode = (int)XfOpCode.SdkCliSplash, OpCodeName = XfOpCode.SdkCliSplash.ToString(), Message = m, }));
        }

        private static string? ToNullIfEmpty(string value)
        {
            return value.Length == 0 ? null : value;
        }
    }
}
