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

using AxCrypt.Abstractions;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.IO;
using AxCrypt.Core.UI;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal class ExportPublicKeyOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!EmailAddress.TryParse(parameters.CurrentOp.Arg1, out EmailAddress _))
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidEmail, "'{0}' is not a valid email.".Format(parameters.CurrentOp.Arg1)));
        }
        var toStore = New<IStandardIoDataStore>(parameters.Arg2);
        if (!New<IFileVerify>().CanWriteToFile(toStore))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotWrite, parameters, "Can't write to file '{0}'.".Format(toStore.Name)));
        }
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        EmailAddress email = EmailAddress.Parse(parameters.Arg1);
        UserPublicKey? userPublicKey = parameters.PublicKeys.FirstOrDefault(pk => pk.Email == email);
        if (userPublicKey == null)
        {
            return Task.FromResult(new Status(XfStatusCode.PublicKeyNotFound, "Did not find a public key for '{0}' to export.".Format(parameters.CurrentOp.Arg1)));
        }

        var publicKeyFile = New<IDataStore>(parameters.CurrentOp.Arg2);
        string json = New<IStringSerializer>().Serialize(userPublicKey);
        using (TextWriter writer = new StreamWriter(publicKeyFile.OpenWrite()))
        {
            writer.Write(json);
        }


        parameters.Logger.Log(new Status(parameters, $"Extracted a public key for '{parameters.CurrentOp.Arg1}' to '{parameters.CurrentOp.Arg2}'."));
        return Task.FromResult(Status.Success);
    }
}
