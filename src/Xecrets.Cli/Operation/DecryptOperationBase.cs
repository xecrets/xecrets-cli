#region Copyright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2025 Svante Seleborg, All Rights Reserved.
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
using Xecrets.Core.Public;

namespace Xecrets.Cli.Operation;

internal abstract class DecryptOperationBase : IExecutionPhases
{
    protected abstract (Status, IFile) ToStore(Parameters parameters, string originalFileName);

    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!parameters.Identities.Any())
        {
            return Task.FromResult(new Status(XfStatusCode.NoPassword, "A password must be provided to decrypt files."));
        }

        IFile fromStore = parameters.DesktopServices.StandardIoFile(parameters.Arg1);
        if (!parameters.DesktopServices.CanReadFromFile(fromStore, out string? reason))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, parameters, $"Can't read from '{fromStore.Name}'. [{reason}]"));
        }

        (Status status, IFile toStore) = ToStore(parameters, "placeholder.tmp");
        if (!status.IsSuccess)
        {
            return Task.FromResult(status);
        }

        if (!toStore.IsStdout && !parameters.DesktopServices.CanWriteToFolder(toStore.ParentFolder))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotWrite, parameters, "Can't write to '{0}'".Format(toStore.ParentFolder.Name)));
        }

        parameters.TotalsTracker.AddWorkItem(fromStore.Length);

        return Task.FromResult(Status.Success);
    }

    public async Task<Status> RealAsync(Parameters parameters)
    {
        IFile fromStore = parameters.DesktopServices.StandardIoFile(parameters.Arg1);

        parameters.Progress.Display = parameters.Arg1;
        parameters.Progress.Report(Progress.TotalAdded(fromStore.Length));
        
        await using Stream fromStream = parameters.AsciiArmor
            ? new AsciiArmorStream(fromStore.OpenRead())
            : fromStore.OpenRead();
        using var decryption = await Decryption.CreateAsync(fromStream, parameters.Identities, parameters.Progress, parameters.CoreServices);
        
        if (!decryption.IsDecryptable)
        {
            return new Status(XfStatusCode.InvalidPassword, parameters, "Could not decrypt '{0}', no suitable password or private key.".Format(parameters.Arg1));
        }

        (Status status, IFile toStore) = ToStore(parameters, decryption.OriginalFileName);
        if (!status.IsSuccess)
        {
            return status;
        }

        await decryption.DecryptToAsync(toStore, parameters.DesktopServices);

        string sourceDisplayName = fromStore.ToDisplayName();
        string destinationDisplayName = toStore.ToDisplayName();

        parameters.Logger.Log(new Status(parameters, "Decrypted '{0}' to '{1}'.".Format(sourceDisplayName, destinationDisplayName))
        {
            OriginalFileName = decryption.OriginalFileName,
            Result = toStore.FullName,
        });

        return Status.Success;
    }
}
