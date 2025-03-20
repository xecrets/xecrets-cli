﻿#region Copyright and GPL License

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

using System.Security.Cryptography;

using AxCrypt.Abstractions;

using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal class JwtSignOperation : IExecutionPhases
{
    /// <summary>
    /// Arguments[0] => Where to write the signed token, i.e. 'license.txt'.
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Task<Status> DryAsync(Parameters parameters)
    {
        var signedTokenStore = New<IStandardIoDataStore>(parameters.Arg1);
        if (!New<IFileVerify>().CanWriteToFile(signedTokenStore))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotWrite, parameters, "Can't write to '{0}'.".Format(signedTokenStore.Name)));
        }

        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        if (parameters.JwtIssuer.Length == 0)
        {
            return Task.FromResult(new Status(XfStatusCode.MissingArgument, "You must specify the issuer of the signed token before the signing operation."));
        }

        if (parameters.JwtAudience.Length == 0)
        {
            return Task.FromResult(new Status(XfStatusCode.MissingArgument, "You must specify the audience of the signed token before the signing operation."));
        }

        if (parameters.JwtDaysUntilExpiration < 0)
        {
            return Task.FromResult(new Status(XfStatusCode.MissingArgument, "You must specify the claims of the signed token before the signing operation."));
        }

        if (!parameters.JwtClaims.Any())
        {
            return Task.FromResult(new Status(XfStatusCode.MissingArgument, "You must specify the claims of the signed token before the signing operation."));
        }

        var now = New<INow>().Utc;
        var handler = new JsonWebTokenHandler();

        var key = ECDsa.Create();
        key.ImportFromPem(parameters.JwtPrivateKeyPem.Replace("\\n", Environment.NewLine));
        string token = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = parameters.JwtIssuer,
            Audience = parameters.JwtAudience,
            NotBefore = now,
            Expires = now.AddDays(parameters.JwtDaysUntilExpiration),
            IssuedAt = now,
            Claims = parameters.JwtClaims,
            SigningCredentials = new SigningCredentials(new ECDsaSecurityKey(key), SecurityAlgorithms.EcdsaSha256)
        });

        var signedTokenStore = New<IStandardIoDataStore>(parameters.Arg1);
        using (TextWriter writer = new StreamWriter(signedTokenStore.OpenWrite()))
        {
            writer.Write(token);
        }

        return Task.FromResult(Status.Success);
    }
}
