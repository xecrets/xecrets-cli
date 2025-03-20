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

using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

using AxCrypt.Abstractions;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Implementation;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;
using Xecrets.Slip39;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal class Slip39InformationOperation : IExecutionPhases
{
    [AllowNull]
    private IStandardIoDataStore _toStore;

    public Task<Status> DryAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => VerifyOperationInternal(parameters));

    public Task<Status> RealAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => RealVerifyOperationInternal(parameters));

    private Status RealVerifyOperationInternal(Parameters parameters)
    {
        Status status = VerifyOperationInternal(parameters);
        if (!status.IsSuccess)
        {
            return status;
        }

        List<Slip39Prefix> prefixes = [];
        foreach (string shareString in parameters.Slip39.Shares)
        {
            Share share = Share.Parse(shareString);

            Slip39Prefix prefix = new(
                Id: share.Prefix.Id,
                Extendable: share.Prefix.Extendable,
                IterationExponent: share.Prefix.IterationExponent,
                GroupIndex: share.Prefix.GroupIndex,
                GroupThreshold: share.Prefix.GroupThreshold,
                GroupCount: share.Prefix.GroupCount,
                MemberIndex: share.Prefix.MemberIndex,
                MemberThreshold: share.Prefix.MemberThreshold
            );
            prefixes.Add(prefix);
        }
        OutputVerification(new Slip39Prefixes([.. prefixes]), parameters, _toStore);
        return Status.Success;
    }

    private static void OutputVerification(Slip39Prefixes prefixes, Parameters parameters, IStandardIoDataStore toStore)
    {
        if (parameters.ProgrammaticUse)
        {
            string sharesJson = JsonSerializer.Serialize(prefixes, typeof(Slip39Prefixes),
                SourceGenerationContext.Indented);

            using Stream stream = toStore.OpenWrite();
            byte[] bytes = Encoding.UTF8.GetBytes(sharesJson);
            stream.Write(bytes, 0, bytes.Length);
        }
        else
        {
            using StreamWriter stream = new(toStore.OpenWrite());
            foreach (Slip39Prefix prefix in prefixes.Prefixes)
            {
                string info =
                    $"Id: {prefix.Id}" +
                    $"{(prefix.Extendable ? ", Extendable" : "")}" +
                    $", Exponent: {prefix.IterationExponent}" +
                    $", Group(Index: {prefix.GroupIndex}" +
                    $", Threshold: {prefix.GroupThreshold}" +
                    $", Count: {prefix.GroupThreshold})" +
                    $", Member(Index: {prefix.MemberIndex}" +
                    $", Threshold: {prefix.MemberThreshold})";
                stream.WriteLine(info);
            }
        }
    }

    private Status VerifyOperationInternal(Parameters parameters)
    {
        if (parameters.Slip39.Shares.Count == 0)
        {
            return new Status(XfStatusCode.InvalidOption, parameters, "Missing {share(s)}.");
        }

        string toStore = parameters.Arg1.Length > 0 ? parameters.Arg1 : "+";
        _toStore = New<IStandardIoDataStore>(toStore);
        if (!New<IFileVerify>().CanWriteToFile(_toStore))
        {
            return new Status(XfStatusCode.CannotWrite, parameters,
                "The file path '{0}' cannot be written to.".Format(_toStore.Name));
        }

        return Status.Success;
    }
}
