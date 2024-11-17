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

using AxCrypt.Api.Model;
using AxCrypt.Core.Crypto;
using AxCrypt.Core.Extensions;
using AxCrypt.Core.Service;
using AxCrypt.Core.UI;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal class LoadPrivateKeysOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        var fileStore = New<IStandardIoDataStore>(parameters.Arg1);
        if (!New<IFileVerify>().CanReadFromFile(fileStore))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, parameters,
                $"Can't read from file '{fileStore.Name}'."));
        }

        if (parameters.Arg2.Length == 0)
        {
            return Task.FromResult(Status.Success);
        }

        fileStore = New<IStandardIoDataStore>(parameters.Arg2);
        if (!New<IFileVerify>().CanWriteToFile(fileStore))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotWrite, parameters,
                $"Can't write to file '{fileStore.Name}'."));
        }
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        return Task.FromResult(RealAsyncInternal(parameters));
    }

    private static Status RealAsyncInternal(Parameters parameters)
    {
        var store = New<IStandardIoDataStore>(parameters.Arg1);
        UserAccounts? userAccounts;
        using (StreamReader reader = new StreamReader(store.OpenRead()))
        {
            try
            {
                userAccounts = UserAccounts.DeserializeFrom(reader);
            }
            catch (Exception ex)
            {
                return new Status(XfStatusCode.DeserializeError, parameters,
                    $"Deserialization error with '{store.Name}'. {ex.Message}");
            }
        }

        if (userAccounts == null)
        {
            return new Status(XfStatusCode.DeserializeError, parameters,
                $"Deserialization error with '{store.Name}'.");
        }

        UserAccounts? reEncryptedAccounts = ReEncryptAccounts(parameters, userAccounts);
        if (reEncryptedAccounts == null)
        {
            return Status.Success;
        }

        store = New<IStandardIoDataStore>(parameters.Arg2);
        using (StreamWriter writer = new StreamWriter(store.OpenWrite()))
        {
            reEncryptedAccounts.SerializeTo(writer);
        }
        return Status.Success;
    }

    private static UserAccounts? ReEncryptAccounts(Parameters parameters, UserAccounts userAccounts)
    {
        if (userAccounts.Accounts.Count == 0)
        {
            return userAccounts;
        }

        List<Passphrase> passphrases = [.. parameters.Identities
            .Where(i => i.Passphrase != Passphrase.Empty)
            .Select(i => i.Passphrase)];

        List<UserKeyPair> userKeyPairs = [];
        List<AccountKey> nonDecryptableAccountKeys = [];
        foreach (AccountKey key in userAccounts.Accounts.Select(a => a.AccountKeys).SelectMany(a => a))
        {
            if (TryDecryptKey(key, passphrases, out UserKeyPair? userKeyPair))
            {
                userKeyPairs.Add(userKeyPair!);
                continue;
            }
            nonDecryptableAccountKeys.Add(key);
        }
        userKeyPairs = [.. userKeyPairs.OrderByDescending(k => k.Timestamp)];
        if (userKeyPairs.Count != 0)
        {
            parameters.Identities.Add(new LogOnIdentity(userKeyPairs, Passphrase.Empty));
        }
        if (parameters.Arg2.Length == 0)
        {
            return null;
        }

        EmailAddress mainUserEmail = parameters.Identities
            .FirstOrDefault(i => i.UserEmail != EmailAddress.Empty)?.UserEmail ?? EmailAddress.Empty;
        string userEmail = mainUserEmail == EmailAddress.Empty
            ? userAccounts.Accounts.First().UserName : mainUserEmail.Address;

        UserAccount reEncryptedAccount = new UserAccount(userEmail)
        {
            // This turned out to be the easiest way to avoid writing these fields to the JSON file.
            // Modifying the serialization to exclude empty strings was non-trivial, because of constraints
            // when using compile-time source generation for trimmer friendly serialization.
            Tag = null!,
            Signature = null!,
        };
        Passphrase firstPassphrase = parameters.Identities.First(i => i.Passphrase != Passphrase.Empty).Passphrase;
        foreach (UserKeyPair keyPair in userKeyPairs)
        {
            reEncryptedAccount.AccountKeys.Add(keyPair.ToAccountKey(firstPassphrase));
        }
        foreach (AccountKey key in nonDecryptableAccountKeys)
        {
            key.Status = PrivateKeyStatus.PassphraseUnknown;
            reEncryptedAccount.AccountKeys.Add(key);
        }
        UserAccounts reEncryptedAccounts = new()
        {
            Accounts = [reEncryptedAccount],
        };
        return reEncryptedAccounts;
    }

    private static bool TryDecryptKey(AccountKey key, List<Passphrase> passphrases, out UserKeyPair? userKeyPair)
    {
        userKeyPair = null;
        for (int i = 0; i < passphrases.Count; i++)
        {
            userKeyPair = key.ToUserKeyPair(passphrases[i]);
            if (userKeyPair == null)
            {
                continue;
            }

            // Move the good passphrase to the front of the list.
            if (i > 0)
            {
                passphrases.Insert(0, passphrases[i]);
                passphrases.RemoveAt(i + 1);
            }

            return true;
        }
        return false;
    }
}
