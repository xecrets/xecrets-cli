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

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Implementation;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal abstract class DecryptOperationBase : IExecutionPhases
{
    protected abstract (Status, IStandardIoDataStore) ToStore(Parameters parameters, string originalFileName);

    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!parameters.Identities.Any())
        {
            return Task.FromResult(new Status(XfStatusCode.NoPassword, "A password must be provided to decrypt files."));
        }

        IStandardIoDataStore fromStore = New<IStandardIoDataStore>(parameters.Arg1);
        if (!New<IFileVerify>().CanReadFromFile(fromStore))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, parameters, "Can't read from '{0}'.".Format(fromStore.Name)));
        }

        (Status status, IStandardIoDataStore toStore) = ToStore(parameters, "placeholder.tmp");
        if (!status.IsSuccess)
        {
            return Task.FromResult(status);
        }

        if (!toStore.IsStdout && !New<IFileVerify>().CanWriteToFolder(toStore.Container))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotWrite, parameters, "Can't write to '{0}'".Format(toStore.Container.Name)));
        }

        parameters.TotalsTracker.AddWorkItem(fromStore.Length());

        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        IStandardIoDataStore fromStore = New<IStandardIoDataStore>(parameters.Arg1);

        parameters.Progress.Display = parameters.Arg1;
        if (parameters.AsciiArmor)
        {
            fromStore = new AsciiArmorDataStore(fromStore);
        }
        using (var decryption = new Decryption(fromStore.OpenRead(), parameters.Identities, parameters.Progress))
        {
            if (!decryption.HasValidPassphrase)
            {
                return Task.FromResult(new Status(XfStatusCode.InvalidPassword, parameters, "Could not decrypt '{0}', no suitable password or private key.".Format(parameters.Arg1)));
            }

            (Status status, IStandardIoDataStore toStore) = ToStore(parameters, decryption.OriginalFileName);
            if (!status.IsSuccess)
            {
                return Task.FromResult(status);
            }

            decryption.DecryptTo(toStore);

            string sourceDisplayName = fromStore.ToDisplayName();
            string destinationDisplayName = toStore.ToDisplayName();

            parameters.Logger.Log(new Status(parameters, "Decrypted '{0}' to '{1}'.".Format(sourceDisplayName, destinationDisplayName))
            {
                OriginalFileName = decryption.OriginalFileName,
                Result = toStore.FullName,
            });
        }

        return Task.FromResult(Status.Success);
    }
}
