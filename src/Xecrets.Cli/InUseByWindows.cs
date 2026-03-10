#region Copyright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2026 Svante Seleborg, All Rights Reserved.
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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

using Xecrets.Cli.Abstractions;

namespace Xecrets.Cli;

[SuppressMessage("ReSharper", "InconsistentNaming")]
internal sealed class InUseByWindows : IInUseBy
{
    private const int ErrorSuccess = 0;
    private const int ErrorMoreData = 234;
    private const int CchRmSessionKey = 32;

    public string Path(string path)
    {
        StringBuilder sessionKey = new(CchRmSessionKey + 1);

        int startResult = RmStartSession(out var sessionHandle, 0, sessionKey);
        if (startResult != ErrorSuccess)
        {
            return "Unknown process (RmStartSession)";
        }

        try
        {
            string[] resources = [path];
            int registerResult = RmRegisterResources(sessionHandle, (uint)resources.Length, resources, 0, null, 0, null);
            if (registerResult != ErrorSuccess)
            {
                return "Unknown process (RmRegisterResources)";
            }

            uint processInfoCount = 0;

            int getListResult = RmGetList(sessionHandle, out var processInfoNeeded, ref processInfoCount, null, out _);
            if (getListResult == ErrorSuccess && processInfoNeeded == 0)
            {
                return string.Empty;
            }

            if (getListResult != ErrorMoreData)
            {
                return string.Empty;
            }

            RM_PROCESS_INFO[] processInfos = new RM_PROCESS_INFO[processInfoNeeded];
            processInfoCount = processInfoNeeded;

            getListResult = RmGetList(sessionHandle, out processInfoNeeded, ref processInfoCount, processInfos, out _);
            if (getListResult != ErrorSuccess)
            {
                return "Unknown process (RmGetList)";
            }

            List<string> processDescriptions = [];

            for (int i = 0; i < processInfoCount; i++)
            {
                RM_PROCESS_INFO processInfo = processInfos[i];
                int processId = processInfo.Process.dwProcessId;

                string processName = string.IsNullOrWhiteSpace(processInfo.strAppName) ? "Unknown process" : processInfo.strAppName;
                string processPath = "Unknown path";

                try
                {
                    using Process process = Process.GetProcessById(processId);
                    processName = string.IsNullOrWhiteSpace(process.ProcessName) ? processName : process.ProcessName;
                    processPath = process.MainModule?.FileName ?? processPath;
                }
                catch
                {
                    // ignored
                }

                processDescriptions.Add($"{processName} ({processPath})");
            }

            return processDescriptions.Count == 0
                ? string.Empty
                : $"In use by: {string.Join(", ", processDescriptions)}";
        }
        finally
        {
            _ = RmEndSession(sessionHandle);
        }
    }

    [DllImport("rstrtmgr", CharSet = CharSet.Unicode)]
    private static extern int RmStartSession(out uint pSessionHandle, int dwSessionFlags, StringBuilder strSessionKey);

    [DllImport("rstrtmgr", CharSet = CharSet.Unicode)]
    [SuppressMessage("Interoperability", "SYSLIB1054:Use \'LibraryImportAttribute\' instead of \'DllImportAttribute\' to generate P/Invoke marshalling code at compile time")]
    private static extern int RmRegisterResources(
        uint dwSessionHandle,
        uint nFiles,
        string[] rgsFilenames,
        uint nApplications,
        [In] RM_UNIQUE_PROCESS[]? rgApplications,
        uint nServices,
        string[]? rgsServiceNames);

    [DllImport("rstrtmgr", CharSet = CharSet.Unicode)]
    [SuppressMessage("Interoperability", "SYSLIB1054:Use \'LibraryImportAttribute\' instead of \'DllImportAttribute\' to generate P/Invoke marshalling code at compile time")]
    private static extern int RmGetList(
        uint dwSessionHandle,
        out uint pnProcInfoNeeded,
        ref uint pnProcInfo,
        [In, Out] RM_PROCESS_INFO[]? rgAffectedApps,
        out uint lpdwRebootReasons);

    [DllImport("rstrtmgr", CharSet = CharSet.Unicode)]
    [SuppressMessage("Interoperability", "SYSLIB1054:Use \'LibraryImportAttribute\' instead of \'DllImportAttribute\' to generate P/Invoke marshalling code at compile time")]
    private static extern int RmEndSession(uint pSessionHandle);

    [StructLayout(LayoutKind.Sequential)]
    private struct RM_UNIQUE_PROCESS
    {
        public int dwProcessId;
        public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
    }

    private enum RM_APP_TYPE
    {
        RmUnknownApp = 0,
        RmMainWindow = 1,
        RmOtherWindow = 2,
        RmService = 3,
        RmExplorer = 4,
        RmConsole = 5,
        RmCritical = 1000,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct RM_PROCESS_INFO
    {
        public RM_UNIQUE_PROCESS Process;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string strAppName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string strServiceShortName;

        public RM_APP_TYPE ApplicationType;
        public uint AppStatus;
        public uint TSSessionId;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bRestartable;
    }
}
