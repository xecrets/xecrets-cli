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

internal class Slip39SplitOperation : IExecutionPhases
{
    private string _shareFormat = string.Empty;

    [AllowNull]
    private IStandardIoDataStore _toStore;

    public Task<Status> DryAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => SplitOperationInternal(parameters));

    public Task<Status> RealAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => RealSplitOperationInternal(parameters));

    private Status RealSplitOperationInternal(Parameters parameters)
    {
        Status status = SplitOperationInternal(parameters);
        if (!status.IsSuccess)
        {
            return status;
        }

        StringEncoding stringEncoding = StringEncoding.None;
        switch (parameters.Slip39.SecretFormat)
        {
            case XfOptionKeys.Bip39:
                stringEncoding = StringEncoding.Bip39;
                break;
            case XfOptionKeys.Hex:
                stringEncoding = StringEncoding.Hex;
                break;
            case XfOptionKeys.Slip39:
                break;
            case XfOptionKeys.Text:
                stringEncoding = StringEncoding.None;
                break;
            case XfOptionKeys.Base64:
                stringEncoding = StringEncoding.Base64;
                break;
            default:
                return new Status(XfStatusCode.InvalidOption,
                    parameters, $"Invalid secret format '{parameters.Slip39.SecretFormat}'.");
        }

        IShamirsSecretSharing sss = New<IShamirsSecretSharing>();
        byte[] masterSecretBytes = parameters.Slip39.Secret.ToSecretBytes(stringEncoding);
        Group[] groups = parameters.Slip39.Groups.Select(g => new Group(g.Threshold, g.Length)).ToArray();

        Share[][] shares = sss.GenerateShares(true, parameters.Slip39.Exponent, parameters.Slip39.GroupThreshold,
            groups, parameters.Slip39.Password, masterSecretBytes);

        Slip39Split slip39Split = new(
            Description: $"Group threshold {parameters.Slip39.GroupThreshold}.",
            Groups: shares.Select((g, gi) => new Slip39SplitGroup(
                Description: $"Group {gi + 1} of {shares.Length} with threshold {groups[gi].GroupThreshold}.",
                Shares: g.Select(s => new Slip39SplitShare(s.ToString(_shareFormat))).ToArray())).ToArray());

        if (parameters.ProgrammaticUse)
        {
            string sharesJson = JsonSerializer.Serialize(slip39Split, typeof(Slip39Split),
                SourceGenerationContext.Indented);

            using Stream stream = _toStore.OpenWrite();
            byte[] bytes = Encoding.UTF8.GetBytes(sharesJson);
            stream.Write(bytes, 0, bytes.Length);
        }
        else
        {
            using StreamWriter stream = new(_toStore.OpenWrite());
            if (slip39Split.Groups.Length > 1)
            {
                stream.WriteLine(slip39Split.Description);
            }
            for (int i = 0; i < slip39Split.Groups.Length; ++i)
            {
                if (i > 0)
                {
                    stream.WriteLine();
                }
                stream.WriteLine(slip39Split.Groups[i].Description);
                foreach (var share in slip39Split.Groups[i].Shares)
                {
                    stream.WriteLine(share.Value);
                }
            }
        }
        return Status.Success;
    }

    private Status SplitOperationInternal(Parameters parameters)
    {
        if (parameters.Slip39.GroupThreshold < 1 || parameters.Slip39.GroupThreshold > parameters.Slip39.Groups.Count)
        {
            return new Status(XfStatusCode.InvalidOption, parameters,
                $"Group threshold '{parameters.Slip39.GroupThreshold}' must be >= 1 and <= number of groups.");
        }
        if (parameters.Slip39.Secret.Length == 0)
        {
            return new Status(XfStatusCode.InvalidOption, parameters, "No secret to split was specified.");
        }

        string format = parameters.Arg1;

        if (format.Length == 0)
        {
            format = XfOptionKeys.Slip39.ToString();
        }

        XfOptionKeys formatOption = XfOptionKeys.Slip39;
        if (format.Length > 0 && !Enum.TryParse(format, ignoreCase: true, out formatOption))
        {
            return new Status(XfStatusCode.InvalidOption, parameters, $"Unknown format '{format}'.");
        }

        switch (formatOption)
        {
            case XfOptionKeys.Slip39:
                _shareFormat = "G";
                break;
            case XfOptionKeys.Hex:
                _shareFormat = "X";
                break;
            case XfOptionKeys.Base64:
                _shareFormat = "64";
                break;
            default:
                return new Status(XfStatusCode.InvalidOption, parameters, $"Invalid format '{format}'.");
        }

        string toStore = parameters.Arg2.Length > 0 ? parameters.Arg2 : "+";
        _toStore = New<IStandardIoDataStore>(toStore);
        if (!New<IFileVerify>().CanWriteToFile(_toStore))
        {
            return new Status(XfStatusCode.CannotWrite, parameters,
                "The file path '{0}' cannot be written to.".Format(_toStore.Name));
        }

        return Status.Success;
    }
}
