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
using AxCrypt.Core.Crypto;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.Extensions;
using AxCrypt.Core.IO;
using AxCrypt.Core.Service;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal class UseKeyPairOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!parameters.Identities.Any())
        {
            return Task.FromResult(new Status(XfStatusCode.NoPassword, "A password must be provided to access an encrypted key pair file."));
        }
        var fromStore = New<IStandardIoDataStore>(parameters.Arg1);
        if (!New<IFileVerify>().CanReadFromFile(fromStore))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, "Can't read from file '{0}'.".Format(fromStore.Name)));
        }
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        byte[] keyPairFile = New<IDataStore>(parameters.Arg1).ToArray();
        UserKeyPair? keyPair = null;

        IList<LogOnIdentity> identities = parameters.Identities;
        LogOnIdentity? identity = null;
        int index;

        for (index = 0; index < identities.Count; ++index)
        {
            identity = identities[index];
            if (identity.Passphrase != Passphrase.Empty && UserKeyPair.TryLoad(keyPairFile, identity.Passphrase, out keyPair))
            {
                break;
            }
        }

        if (keyPair == null)
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidPassword, parameters, "No valid password was provided to decrypt the key pair."));
        }

        identities[index] = new LogOnIdentity(identity!.KeyPairs.Concat([keyPair]), identity!.Passphrase);

        parameters.Logger.Log(new Status(parameters, "Loaded a key pair created {3} with tag '{2}' for '{1}' from '{0}'".Format(parameters.CurrentOp.Arg1, keyPair.UserEmail, keyPair.KeyPair.PublicKey.Tag, keyPair.Timestamp.ToLocalTime()))
        {
            Utc = keyPair.Timestamp,
        });

        UserPublicKey userPublicKey = new UserPublicKey(keyPair.UserEmail, keyPair.KeyPair.PublicKey);
        parameters.LoadedPublicKeys.AddOrReplace(userPublicKey);
        parameters.Logger.Log(new Status(parameters, "Loaded a public key for '{0}' from '{1}'.".Format(userPublicKey.Email, parameters.Arg1)));

        parameters.SharingEmails.Add(userPublicKey.Email);

        return Task.FromResult(Status.Success);
    }
}
