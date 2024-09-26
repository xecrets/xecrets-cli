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

using System.Security.Cryptography;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class JwtCreateKeyPairOperation : IExecutionPhases
{
    /// <summary>
    /// Arguments[0] => Private key file PEM, i.e. 'private.pem'
    /// Arguments[1] => Public key file PEM, i.e. 'public.pem'
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Task<Status> DryAsync(Parameters parameters)
    {
        IStandardIoDataStore privateFreeStore = parameters.Arg1.FindFree(parameters);
        if (!privateFreeStore.VerifyCanWrite(parameters, out Status status))
        {
            return Task.FromResult(status);
        }

        if (parameters.Arguments.Count == 1)
        {
            return Task.FromResult(Status.Success);
        }

        IStandardIoDataStore publicFreeStore = parameters.Arguments[1].FindFree(parameters);
        if (!publicFreeStore.VerifyCanWrite(parameters, out status))
        {
            return Task.FromResult(status);
        }

        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        IStandardIoDataStore privateFreeStore = parameters.Arg1.FindFree(parameters);
        if (!privateFreeStore.VerifyCanWrite(parameters, out Status status))
        {
            return Task.FromResult(status);
        }

        ECDsa key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        Span<char> keySpan = new Span<char>(new char[500]);
        if (!key.TryExportPkcs8PrivateKeyPem(keySpan, out int charsWritten))
        {
            return Task.FromResult(new Status(Public.XfStatusCode.InternalError, "Couldn't export the PKCS8 Private Key."));
        }

        using (StreamWriter writer = new(privateFreeStore.OpenWrite()))
        {
            writer.Write(keySpan.Slice(0, charsWritten));
        }

        if (parameters.Arguments.Count == 1)
        {
            return Task.FromResult(Status.Success);
        }

        IStandardIoDataStore publicFreeStore = parameters.Arguments[1].FindFree(parameters);
        if (!publicFreeStore.VerifyCanWrite(parameters, out status))
        {
            return Task.FromResult(status);
        }

        string publicPem = key.ExportSubjectPublicKeyInfoPem();
        using (StreamWriter writer = new(publicFreeStore.OpenWrite()))
        {
            if (privateFreeStore.IsStdout && publicFreeStore.IsStdout)
            {
                writer.WriteLine();
            }
            writer.Write(publicPem);
        }

        return Task.FromResult(Status.Success);
    }
}
