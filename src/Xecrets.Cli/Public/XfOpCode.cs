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
        CreateKeyPair = 1090,
        DecryptTo = 1100,
        DecryptToFolder = 1110,
        DefaultInternal = 1120,
        DryRun = 1130,
        Echo = 1140,
        EncryptTo = 1150,
        End = 1160,
        EnvironmentOption = 1170,
        ExportPublicKey = 1180,
        GplLicense = 1190,
        Help = 1200,
        Id = 1210,
        Internal = 1220,
        JwtAudience = 1230,
        JwtClaims = 1240,
        JwtCreateKeyPair = 1250,
        JwtIssuer = 1260,
        JwtPrivateKey = 1270,
        JwtSign = 1280,
        JwtVerify = 1290,
        LoadPublicKey = 1300,
        NoLog = 1310,
        NoOverwrite = 1320,
        NoProgress = 1330,
        OptionsCodeExport = 1340,
        OptionsFromFile = 1350,
        Overwrite = 1360,
        Password = 1370,
        Progressing = 1380,
        ProgressLog = 1390,
        Quiet = 1400,
        Stdout = 1410,
        TextLog = 1420,
        UseKeyPair = 1430,
        UsePublicKey = 1440,
        Wipe = 1450,
        #endregion Possible-to-change values if API version is updated
    }
}
