#region Coypright and GPL License

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

#endregion Coypright and GPL License

using Xecrets.Cli.Log;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli
{
    internal class Executor(Parameters parameters) : IDisposable
    {
        public async Task<Status> RunAsync()
        {
            Status status = await RunAsync(new DryRunFactory(parameters));
            if ((parameters.Parser.IsQuiet || parameters.Parser.Internal) && status.IsSuccess)
            {
                New<Splash>().Clear();
            }

            if (!status.IsSuccess)
            {
                return status;
            }

            if (parameters.Parser.IsDryRunOnly)
            {
                return new Status("A successful dry run was executed, no files were changed.");
            }

            ResetParametersForRealRun();

            return await RunAsync(new RealRunFactory(parameters));
        }

        private void ResetParametersForRealRun()
        {
            parameters.TotalsTracker.ResetLogger(parameters);
            parameters.Overwrite = false;
            parameters.Compress = true;
        }

        private static readonly XfStatusCode[] RecoverableInSequence = [XfStatusCode.InvalidPassword];

        private static async Task<Status> RunAsync(RunFactory factory)
        {
            int skipLevel = 0;
            OptionsParser parser = factory.Parameters.Parser;
            foreach (ParsedOp parsedOp in parser.ParsedOps)
            {
                factory.Parameters.CurrentOp = parsedOp;
                if (skipLevel > 0 && parsedOp.OpCode != XfOpCode.End && parsedOp.OpCode != XfOpCode.Begin)
                {
                    continue;
                }

                Status status = await factory.Create(parsedOp.OpCode).DoAsync();
                if (skipLevel > parser.OpLevel)
                {
                    skipLevel = 0;
                }

                // If the operation failed and it's recoverable and we're in a sequence, skip the rest of the sequence,
                // and continue instead of aborting and returning the error.
                if (!status.IsSuccess && parser.OpLevel > 0 && RecoverableInSequence.Contains(status.StatusCode))
                {
                    factory.Parameters.Logger.Log(status);
                    skipLevel = parser.OpLevel;
                    continue;
                }

                if (!status.IsSuccess)
                {
                    return status;
                }
            }

            if (parser.OpLevel > 0)
            {
                return new Status(XfStatusCode.BadSequence, "Begin sequence without matching end.");
            }

            return parser.ParseStatus;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }

            parameters.Dispose();
            _disposed = true;
        }
    }
}
