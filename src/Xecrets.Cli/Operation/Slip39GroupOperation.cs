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

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class Slip39GroupOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => GroupOperationInternal(parameters));

    public Task<Status> RealAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => GroupOperationInternal(parameters));

    private static Status GroupOperationInternal(Parameters parameters)
    {
        if (!int.TryParse(parameters.Arg1, out int threshold))
        {
            return new Status(XfStatusCode.InvalidOption,
                parameters, $"Threshold '{parameters.Arg1}' must be an integer.");
        }
        if (!int.TryParse(parameters.Arg2, out int shares))
        {
            return new Status(XfStatusCode.InvalidOption,
                parameters, $"The number of group shares '{parameters.Arg2}' must be an integer.");
        }
        if (threshold < 1 || threshold > 16 || threshold > shares || shares < 1)
        {
            return new Status(XfStatusCode.InvalidOption, parameters,
                $"The group threshold {threshold} must be between 1 and 16, and <= shares ({shares}) which must be >= 1.");
        }
        if (threshold == 1 && shares > 1)
        {
            return new Status(XfStatusCode.InvalidOption, parameters,
                $"When there are > 1 member in a group, the member threshold must be > 1");
        }

        parameters.Slip39.Groups.Add(new(Threshold: threshold, Shares: shares));

        return Status.Success;
    }
}
