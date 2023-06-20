﻿#region Coypright and GPL License

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

using AxCrypt.Abstractions;
using AxCrypt.Core;
using AxCrypt.Core.Crypto;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.IO;
using AxCrypt.Core.Service;
using AxCrypt.Core.UI;

using Xecrets.File.Cli.Abstractions;
using Xecrets.File.Cli.Public;
using Xecrets.File.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.File.Cli.Operation
{
    internal class CreateKeyPairOperation : IExecutionPhases
    {
        public Status Dry(Parameters parameters)
        {
            if (!parameters.Identities.Any())
            {
                return new Status(XfStatusCode.NoPassword, parameters, "A password must be provided to create a key pair.");
            }

            if (!EmailAddress.TryParse(parameters.Email, out EmailAddress _))
            {
                return new Status(XfStatusCode.InvalidEmail, parameters, "'{0}' is not a valid email address.".Format(parameters.Email));
            }

            var toStore = New<IStandardIoDataStore>(parameters.To);
            if (!New<IFileVerify>().CanWriteToFile(toStore))
            {
                return new Status(XfStatusCode.CannotWrite, parameters, "The file path '{0}' cannot be written to.".Format(toStore.Name));
            }

            return Status.Success;
        }

        public Status Real(Parameters parameters)
        {
            parameters.Logger.Log(XfOpCode.Progressing, new Status(parameters, "Generating a key pair may take some time, please be patient."));

            EmailAddress email = EmailAddress.Parse(parameters.CurrentOp.Email);
            IAsymmetricKeyPair keyPair = Resolve.AsymmetricFactory.CreateKeyPair(4096);
            UserKeyPair userKeyPair = new UserKeyPair(email, New<INow>().Utc, keyPair);
            byte[] encryptedBytes = userKeyPair.ToArray(parameters.Identities.First().Passphrase);
            IDataStore destination = New<IStandardIoDataStore>(parameters.CurrentOp.To);
            using (Stream stream = destination.OpenWrite())
            {
                stream.Write(encryptedBytes, 0, encryptedBytes.Length);
            }

            LogOnIdentity identity = parameters.Identities[0];
            parameters.Identities[0] = new LogOnIdentity(identity.KeyPairs.Concat(new UserKeyPair[] { userKeyPair }), identity.Passphrase);

            parameters.Logger.Log(new Status(parameters, $"Created a key pair for '{parameters.CurrentOp.Email}' in '{parameters.CurrentOp.To}'."));

            return Status.Success;
        }
    }
}
