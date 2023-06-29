#region Coypright and GPL License

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

namespace Xecrets.File.Cli.Public
{
    /// <summary>
    /// The possible operation codes. They should have fixed unchangeable ids as they may be emitted in JSON to an independent
    /// program that needs to be able to interpret the action codes correctly.
    /// </summary>
    /// <remarks>
    /// Uses an 'Xf' prefix to distinguish itself, since its name is very generic.
    /// </remarks>
    public enum XfOpCode
    {
        #region Never-changing values
        None = 0,

        SdkCliError = 10,
        SdkCliSplash = 20,
        SdkCliVersion = 30,
        SdkJsonLog = 40,
        #endregion Never-changing values

        #region Possible-to-change values if API version is updated
        ArgumentMarkdown = 1000,
        CliCrashLog = 1010,
        CliDebugBreak = 1020,
        CliDebugBreakParse = 1030,
        CliLicense = 1040,
        CliPlatform = 1050,
        CliProgramExit = 1060,
        CliTextMessage = 1070,
        CreateKeyPair = 1080,
        DecryptTo = 1090,
        DecryptToFolder = 1100,
        DefaultInternal = 1110,
        DryRun = 1120,
        Echo = 1130,
        EncryptTo = 1140,
        EnvironmentOption = 1150,
        ExportPublicKey = 1160,
        GplLicense = 1170,
        Help = 1180,
        Id = 1190,
        Internal = 1200,
        JwtAudience = 1210,
        JwtClaims = 1220,
        JwtCreateKeyPair = 1230,
        JwtIssuer = 1240,
        JwtPrivateKey = 1250,
        JwtSign = 1260,
        JwtVerify = 1270,
        LoadPublicKey = 1280,
        NoLog = 1290,
        NoOverwrite = 1300,
        NoProgress = 1310,
        OptionsCodeExport = 1320,
        OptionsFromFile = 1330,
        Overwrite = 1340,
        Password = 1350,
        Progressing = 1360,
        ProgressLog = 1370,
        Quiet = 1380,
        Stdout = 1390,
        TextLog = 1400,
        UseKeyPair = 1410,
        UsePublicKey = 1420,
        Wipe = 1430,
        #endregion Possible-to-change values if API version is updated
    }
}
