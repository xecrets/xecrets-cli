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

using System.Security.Cryptography;

using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class JwtVerifyOperation : IExecutionPhases
{
    /// <summary>
    /// Arguments[0] => The public key, i.e. 'public.pem'
    /// Arguments[1] => The signed token, i.e. 'license.txt'.
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Task<Status> DryAsync(Parameters parameters)
    {
        IFile publicPemFile = parameters.DesktopServices.StandardIoFile(parameters.Arguments[0]);
        if (!parameters.DesktopServices.CanReadFromFile(publicPemFile, out string? reason))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, $"Can't read public pem from file '{publicPemFile.Name}'. [{reason}]"));
        }

        IFile signedTokenFile = parameters.DesktopServices.StandardIoFile(parameters.Arguments[1]);
        if (!parameters.DesktopServices.CanReadFromFile(signedTokenFile, out reason))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, $"Can't read signed token from file '{signedTokenFile.Name}'. [{reason}]"));
        }

        return Task.FromResult(Status.Success);
    }

    public async Task<Status> RealAsync(Parameters parameters)
    {
        if (parameters.JwtIssuer.Length == 0)
        {
            return new Status(XfStatusCode.MissingArgument, "The issuer of the signed token is required.");
        }

        if (parameters.JwtAudience.Length == 0)
        {
            return new Status(XfStatusCode.MissingArgument, "The audience of the signed token is required.");
        }

        IFile publicPemFile = parameters.DesktopServices.StandardIoFile(parameters.Arguments[0]);
        string publicPem;
        using (StreamReader reader = new StreamReader(publicPemFile.OpenRead()))
        {
            publicPem = await reader.ReadToEndAsync();
        }

        IFile signedTokenFile = parameters.DesktopServices.StandardIoFile(parameters.Arguments[1]);
        string signedToken;
        using (StreamReader reader = new StreamReader(signedTokenFile.OpenRead()))
        {
            signedToken = await reader.ReadToEndAsync();
        }

        var handler = new JsonWebTokenHandler();

        var key = ECDsa.Create();
        key.ImportFromPem(publicPem);

        TokenValidationResult result = await handler.ValidateTokenAsync(signedToken, new TokenValidationParameters
        {
            ValidIssuer = parameters.JwtIssuer,
            ValidAudience = parameters.JwtAudience,
            IssuerSigningKey = new ECDsaSecurityKey(key)
        });

        if (!result.IsValid)
        {
            return new Status(XfStatusCode.InvalidToken, "Invalid Jwt Token");
        }

        DateTime notBefore = DateTime.UnixEpoch.AddSeconds((int)result.Claims["nbf"]);
        DateTime expires = DateTime.UnixEpoch.AddSeconds((int)result.Claims["exp"]);
        DateTime issuedAt = DateTime.UnixEpoch.AddSeconds((int)result.Claims["iat"]);
        parameters.Logger.Log(new Status(parameters, "Validated token from '{0}' for '{1}' issued at '{2}' valid from '{3}' until '{4}'."
            .Format(result.Issuer, result.Claims["aud"], issuedAt.ToString("u"), notBefore.ToString("u"), expires.ToString("u"))));

        return Status.Success;
    }
}
