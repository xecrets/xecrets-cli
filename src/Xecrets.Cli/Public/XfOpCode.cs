#region Coypright and GPL License

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

#endregion Coypright and GPL License

namespace Xecrets.Cli.Public
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
        Begin = 1010,
        CliCrashLog = 1020,
        CliDebugBreak = 1030,
        CliDebugBreakParse = 1040,
        CliLicense = 1050,
        CliPlatform = 1060,
        CliProgramExit = 1070,
        CliTextMessage = 1080,
        Compress = 1090,
        CreateKeyPair = 1100,
        DecryptTo = 1110,
        DecryptToFolder = 1120,
        DefaultInternal = 1130,
        DryRun = 1140,
        Echo = 1150,
        EncryptTo = 1160,
        End = 1170,
        EnvironmentOption = 1180,
        ExportPublicKey = 1190,
        GplLicense = 1200,
        Help = 1210,
        Id = 1220,
        Internal = 1230,
        JwtAudience = 1240,
        JwtClaims = 1250,
        JwtCreateKeyPair = 1260,
        JwtIssuer = 1270,
        JwtPrivateKey = 1280,
        JwtSign = 1290,
        JwtVerify = 1300,
        LoadPublicKey = 1310,
        NoCompress = 1320,
        NoLog = 1330,
        NoOverwrite = 1340,
        NoProgress = 1350,
        OptionsCodeExport = 1360,
        OptionsFromFile = 1370,
        Overwrite = 1380,
        Password = 1390,
        Progressing = 1400,
        ProgressLog = 1410,
        Quiet = 1420,
        Stdout = 1430,
        TextLog = 1440,
        UseKeyPair = 1450,
        UsePublicKey = 1460,
        Wipe = 1470,
        #endregion Possible-to-change values if API version is updated
    }
}
