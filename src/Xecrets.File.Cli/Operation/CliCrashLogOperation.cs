﻿#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright © 2022-2023, Svante Seleborg, All Rights Reserved.
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

using Xecrets.File.Cli.Abstractions;
using Xecrets.File.Cli.Run;

namespace Xecrets.File.Cli.Operation
{
    internal class CliCrashLogOperation : IExecutionPhases
    {
        public Status Dry(Parameters parameters)
        {
            string crashLogFullName = parameters.Arguments[0];
            if (Directory.Exists(crashLogFullName))
            {
                return new Status(Public.XfStatusCode.NotAFile, $"Crash log file path '{crashLogFullName}' is a folder.");
            }

            string directory = Path.GetDirectoryName(crashLogFullName) ?? string.Empty;
            if (directory.Length > 0 && !Directory.Exists(directory))
            {
                return new Status(Public.XfStatusCode.NotAFolder, $"Crash log file folder '{directory}' does not exist.");
            }

            parameters.CrashLogFile = crashLogFullName;
            return Status.Success;
        }

        public Status Real(Parameters parameters)
        {
            return Status.Success;
        }
    }
}