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
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;

using Xecrets.Cli.Abstractions;

namespace Xecrets.Cli;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SupportedOSPlatform("windows")]
internal sealed unsafe partial class InUseByWindows : IInUseBy
{
    private const int ErrorSuccess = 0;
    private const int ErrorMoreData = 234;
    private const int CchRmSessionKey = 32;

    public string Path(string path)
    {
        char* sessionKey = stackalloc char[CchRmSessionKey + 1];

        int startResult = RmStartSession(out uint sessionHandle, 0, sessionKey);
        if (startResult != ErrorSuccess)
        {
            return "Unknown process (RmStartSession)";
        }

        try
        {
            string[] resources = [path];
            int registerResult = RmRegisterResources(sessionHandle, (uint)resources.Length, resources,
                0, null, 0, null);
            if (registerResult != ErrorSuccess)
            {
                return "Unknown process (RmRegisterResources)";
            }

            uint processInfoCount = 0;

            int getListResult = RmGetList(sessionHandle, out uint processInfoNeeded, ref processInfoCount, null, out _);
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

            List<string> processDescriptions = [];

            fixed (RM_PROCESS_INFO* processInfosPtr = processInfos)
            {
                getListResult = RmGetList(sessionHandle, out processInfoNeeded, ref processInfoCount, processInfosPtr, out _);
                if (getListResult != ErrorSuccess)
                {
                    return "Unknown process (RmGetList)";
                }

                for (int i = 0; i < processInfoCount; i++)
                {
                    RM_PROCESS_INFO processInfo = processInfosPtr[i];
                    int processId = processInfo.Process.dwProcessId;

                    string processName = processInfo.AppName.Length == 0 ? "Unknown process" : processInfo.AppName;
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
            }

            return processDescriptions.Count == 0
                ? string.Empty
                : $"{string.Join(", ", processDescriptions)}";
        }
        finally
        {
            _ = RmEndSession(sessionHandle);
        }
    }

    [LibraryImport("rstrtmgr", EntryPoint = "RmStartSession")]
    private static partial int RmStartSession(
        out uint pSessionHandle,
        int dwSessionFlags,
        char* strSessionKey);

    [LibraryImport("rstrtmgr", EntryPoint = "RmRegisterResources", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int RmRegisterResources(
        uint dwSessionHandle,
        uint nFiles,
        [MarshalUsing(CountElementName = nameof(nFiles))]
        [In] string[] rgsFilenames,
        uint nApplications,
        [MarshalUsing(CountElementName = nameof(nApplications))]
        [In] RM_UNIQUE_PROCESS[]? rgApplications,
        uint nServices,
        [MarshalUsing(CountElementName = nameof(nServices))]
        [In] string[]? rgsServiceNames);

    [LibraryImport("rstrtmgr", EntryPoint = "RmGetList")]
    private static partial int RmGetList(
        uint dwSessionHandle,
        out uint pnProcInfoNeeded,
        ref uint pnProcInfo,
        RM_PROCESS_INFO* rgAffectedApps,
        out uint lpdwRebootReasons);

    [LibraryImport("rstrtmgr", EntryPoint = "RmEndSession")]
    private static partial int RmEndSession(uint pSessionHandle);

    [StructLayout(LayoutKind.Sequential)]
    private struct RM_UNIQUE_PROCESS
    {
        public int dwProcessId;
        public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
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

        public fixed char strAppName[256];

        public fixed char strServiceShortName[64];

        public RM_APP_TYPE ApplicationType;
        public uint AppStatus;
        public uint TSSessionId;
        public int bRestartable;

        public readonly string AppName
        {
            get
            {
                fixed (char* p = strAppName)
                {
                    int length = 0;
                    while (length < 256 && p[length] != '\0')
                    {
                        ++length;
                    }
                    return new string(p, 0, length);
                }
            }
        }
    }
}
