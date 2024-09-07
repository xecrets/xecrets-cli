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

using AxCrypt.Abstractions;
using AxCrypt.Core;
using AxCrypt.Core.IO;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation
{
    internal class WipeOperation : IExecutionPhases
    {
        public Task<Status> DryAsync(Parameters parameters)
        {
            foreach (string file in parameters.Arguments)
            {
                var fileStore = New<IStandardIoDataStore>(file);
                if (!New<IFileVerify>().CanDeleteFile(fileStore))
                {
                    return Task.FromResult(new Status(XfStatusCode.CannotDelete, parameters, "Can't delete '{0}'.".Format(fileStore.Name)));
                }

                parameters.TotalsTracker.AddWorkItem(fileStore.Length());
            }
            return Task.FromResult(Status.Success);
        }

        public Task<Status> RealAsync(Parameters parameters)
        {
            var progress = parameters.Logger.Progress;
            progress.NotifyLevelStart();
            try
            {
                for (int i = 0; i < parameters.Arguments.Count; ++i)
                {
                    string file = parameters.Arguments[i];
                    IDataStore fileStore = New<IDataStore>(file);

                    progress.Display = file;

                    using (FileLock fileLock = New<FileLocker>().Acquire(fileStore))
                    {
                        // The design of the Wipe() method is unfortunate, and causes the complication with
                        // the progress levels etc. Should either be rewritten in the original code base or
                        // just reimplemented independently in a more flexible way.
                        New<AxCryptFile>().Wipe(fileLock, progress);
                    }

                    if (i != parameters.Arguments.Count - 1)
                    {
                        parameters.Logger.Log(new Status(parameters, $"Securely wiped '{file}'."));
                    }
                }
            }
            finally
            {
                progress.NotifyLevelFinished();
            }

            parameters.Logger.Log(new Status(parameters, $"Securely wiped '{parameters.Arguments.Last()}'."));

            return Task.FromResult(Status.Success);
        }
    }
}
