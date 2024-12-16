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

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class CliCrashLogOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        if (parameters.Parser.WorkFolder.Length == 0)
        {
            return Task.FromResult(new Status(XfStatusCode.NotAFolder, "No work folder specified."));
        }

        string crashLogName = parameters.Arguments[0];
        string crashLogFullName = Path.Combine(parameters.Parser.WorkFolder, crashLogName);
        if (Directory.Exists(crashLogFullName))
        {
            return Task.FromResult(new Status(XfStatusCode.NotAFile, $"Crash log file path '{crashLogFullName}' is a folder."));
        }

        string directory = Path.GetDirectoryName(crashLogFullName) ?? string.Empty;
        if (directory.Length > 0 && !Directory.Exists(directory))
        {
            return Task.FromResult(new Status(XfStatusCode.NotAFolder, $"Crash log file folder '{directory}' does not exist."));
        }

        parameters.CrashLogFile = crashLogFullName;
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        return Task.FromResult(Status.Success);
    }
}
