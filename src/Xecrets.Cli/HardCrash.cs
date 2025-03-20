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
using System.Runtime.InteropServices;

/// <summary>
/// A helper function to immediately crash the application, to verify for example that a crash report is generated.
/// </summary>
/// <remarks>
/// See https://learn.microsoft.com/en-us/windows/win32/wer/collecting-user-mode-dumps
///
/// To setup Windows Error Reporting (WER) to collect crash dumps, you can use the registry settings in the file
/// xecrets-cli-wer.reg in the root of the repository. This will create a registry key that will collect crash dumps
/// in the folder %LOCALAPPDATA%\CrashDumps . The crash dumps are stored in the folder with the name of the application
/// and the process id of the crashed process, for example "XecretsCli.exe.1234.dmp". They are full memory dumps, so are
/// typically several hundred megabytes in size, but can easily be opened in Visual Studio or WinDbg for analysis.
///
/// An earlier stage option to get some info on a crash is to set the following environment variables: SET
/// COREHOST_TRACE=1 SET COREHOST_TRACEFILE = trace.txt SET COREHOST_TRACE_VERBOSITY = 4 and then run the application.
/// The trace.txt file will contain some information about the crash.
/// </remarks>
internal static class HardCrash
{
    [DllImport("kernel32.dll")]
    [SuppressMessage("Interoperability",
        "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time",
        Justification = "We don't want this to be pre-compiled.")]
    static extern void RaiseException(uint dwExceptionCode, uint dwExceptionFlags, uint nNumberOfArguments, IntPtr lpArguments);

    public static void Immediately()
    {
        if (OperatingSystem.IsWindows())
        {
            RaiseException(0xC0000005, 0, 0, IntPtr.Zero);
        }
        else
        {
            Environment.FailFast("An immediate hard crash was requested.");
        }
    }
}
