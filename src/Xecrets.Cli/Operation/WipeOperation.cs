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

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class WipeOperation : IExecutionPhases
{
    public async Task<Status> DryAsync(Parameters parameters)
    {
        foreach (string file in parameters.Arguments)
        {
            IFile fileStore = parameters.DesktopServices.StandardIoFile(file);
            if (!await DoWithRetryAsync(() => Task.FromResult(parameters.DesktopServices.CanReadFromFile(fileStore, out string? _))))
            {
                return StatusWithLockedByCheck(parameters, fileStore, fileStore.IsAvailable ? string.Empty : "File doesn't exist");
            }

            parameters.TotalsTracker.AddWorkItem(fileStore.Length);
        }

        return Status.Success;
    }

    public async Task<Status> RealAsync(Parameters parameters)
    {
        parameters.Progress.Report(Progress.LevelStarted());
        try
        {
            for (int i = 0; i < parameters.Arguments.Count; ++i)
            {
                string file = parameters.Arguments[i];
                IFile fileStore = parameters.DesktopServices.StandardIoFile(file);

                parameters.Progress.Display = file;
                parameters.Progress.Report(Progress.TotalAdded(fileStore.Length));

                bool wiped = await DoWithRetryAsync(() => parameters.DesktopServices.WipeAsync(file, parameters.Progress));
                if (!wiped)
                {
                    return StatusWithLockedByCheck(parameters, fileStore, string.Empty);
                }

                if (i != parameters.Arguments.Count - 1)
                {
                    parameters.Logger.Log(new Status(parameters, $"Securely wiped '{file}'."));
                }
            }
        }
        finally
        {
            parameters.Progress.Report(Progress.LevelFinished());
        }

        parameters.Logger.Log(new Status(parameters, $"Securely wiped '{parameters.Arguments.Last()}'."));

        return Status.Success;
    }

    private static Status StatusWithLockedByCheck(Parameters parameters, IFile fileStore, string reason)
    {
        string lockedBy = parameters.CliServices.InUseBy.Path(fileStore.FullName);
        string because = lockedBy.Length > 0
            ? $"because it is locked by '{lockedBy}'"
            : "for unknown reasons";
        reason = reason.Length > 0 ? $" [{reason}]" : string.Empty;
        string msg = $"Can't delete '{fileStore.Name}' {because}.{reason}";
        return new Status(XfStatusCode.CannotDelete, parameters, msg);
    }

    private static async Task<bool> DoWithRetryAsync(Func<Task<bool>> toDo)
    {
        int totalDelay = 0;
        int millisecondsDelay = 10;
        do
        {
            if (await toDo())
            {
                return true;
            }

            await Task.Delay(millisecondsDelay);
            totalDelay += millisecondsDelay;

            millisecondsDelay += millisecondsDelay;
        }
        while (totalDelay + millisecondsDelay < 100);

        return false;
    }
}
