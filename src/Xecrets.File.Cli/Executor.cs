#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright 2023, Svante Seleborg, All Rights Reserved.
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

using Xecrets.File.Cli.Log;
using Xecrets.File.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.File.Cli
{
    internal class Executor : IDisposable
    {
        private readonly Parameters _parameters;

        public Executor(Parameters parameters)
        {
            _parameters = parameters;
        }

        public Status Run()
        {
            Status status = Run(new DryRunFactory(_parameters));
            if ((_parameters.Parser.IsQuiet || _parameters.Parser.Internal) && status.IsSuccess)
            {
                New<Splash>().Clear();
            }
            _parameters.TotalsTracker.ResetLogger(_parameters);

            if (!status.IsSuccess)
            {
                return status;
            }

            if (_parameters.Parser.IsDryRunOnly)
            {
                return new Status("A successful dry run was executed, no files were changed.");
            }

            return Run(new RealRunFactory(_parameters));
        }

        private static Status Run(RunFactory factory)
        {
            foreach (ParsedOp parsedOp in factory.Parameters.Parser.ParsedOps)
            {
                factory.Parameters.CurrentOp = parsedOp;

                Status status = factory.Create(parsedOp.OpCode).Do();

                if (!status.IsSuccess)
                {
                    return status;
                }
            }

            return factory.Parameters.Parser.ParseStatus;
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

            _parameters.Dispose();
            _disposed = true;
        }
    }
}
