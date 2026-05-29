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

using AxCrypt.Abstractions;
using AxCrypt.Core;
using AxCrypt.Core.IO;
using AxCrypt.Core.UI;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal class WipeOperation : IExecutionPhases
{
    public async Task<Status> DryAsync(Parameters parameters)
    {
        foreach (string file in parameters.Arguments)
        {
            var fileStore = New<IStandardIoDataStore>(file);
            if (!await DoWithRetry(() => New<IFileVerify>().CanReadFromFile(fileStore)))
            {
                return StatusWithLockedByCheck(parameters, fileStore);
            }

            parameters.TotalsTracker.AddWorkItem(fileStore.Length());
        }

        return Status.Success;

        static Status StatusWithLockedByCheck(Parameters parameters, IStandardIoDataStore fileStore)
        {
            string lockedBy = New<IInUseBy>().Path(fileStore.FullName);
            string reason = lockedBy.Length > 0
                ? "because it is locked by '{0}'".Format(lockedBy)
                : "for unknown reasons";
            string msg = "Can't delete '{0}' {1}.".Format(fileStore.Name, reason);
            return new Status(XfStatusCode.CannotDelete, parameters, msg);
        }
    }

    public async Task<Status> RealAsync(Parameters parameters)
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
                    await DoWithRetry(() => TryWipe(fileLock, progress));
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

        return Status.Success;
        
        static bool TryWipe(FileLock fileLock, IProgressContext progress)
        {
            const int errorSharingViolation = 32;

            try
            {
                // The design of the Wipe() method is unfortunate, and causes the complication with
                // the progress levels etc. Should either be rewritten in the original code base or
                // just reimplemented independently in a more flexible way.
                New<AxCryptFile>().Wipe(fileLock, progress);
            }
            catch (IOException ioex) when ((ioex.HResult & 0xFFFF) == errorSharingViolation)
            {
                return false;
            }

            return true;
        }
    }

    private static async Task<bool> DoWithRetry(Func<bool> toDo)
    {
        int totalDelay = 0;
        int millisecondsDelay = 10;
        do
        {
            if (toDo())
            {
                return true;
            }
            
            await Task.Delay(millisecondsDelay);
            totalDelay += millisecondsDelay;

            millisecondsDelay += millisecondsDelay;
        }
        while (totalDelay + millisecondsDelay < 100) ;

        return false;
    }
}
