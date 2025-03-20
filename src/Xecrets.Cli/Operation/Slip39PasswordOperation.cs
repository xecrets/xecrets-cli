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

internal class Slip39PasswordOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => PasswordOperationInternal(parameters));

    public Task<Status> RealAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => PasswordOperationInternal(parameters));

    private static Status PasswordOperationInternal(Parameters parameters)
    {
        string password = parameters.Arg1;
        if (password.Length == 0)
        {
            return new Status(XfStatusCode.InvalidOption, parameters, "Missing {password}.");
        }
        string exp = parameters.Arg2.Length > 0 ? parameters.Arg2 : "4";
        if (!int.TryParse(exp, out int exponent))
        {
            return new Status(XfStatusCode.InvalidOption, parameters,
                $"The iteration exponent '{parameters.Arg2}' must be an integer.");
        }
        if (exponent is < 0 or > 15)
        {
            return new Status(XfStatusCode.InvalidOption, parameters,
                $"The iteration exponent '{exponent}' must be between 1 and 15.");
        }

        parameters.Slip39.Password = password;
        parameters.Slip39.Exponent = exponent;

        return Status.Success;
    }
}
