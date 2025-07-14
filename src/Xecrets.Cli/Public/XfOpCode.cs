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

namespace Xecrets.Cli.Public;

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
    SdkSigInt = 50,
    #endregion Never-changing values

    #region Possible-to-change values if API version is updated
    ArgumentMarkdown = 1000,
    AsciiArmor = 1010,
    Begin = 1020,
    CliCrashLog = 1030,
    CliDebugBreak = 1040,
    CliDebugBreakParse = 1050,
    CliLicense = 1060,
    CliPlatform = 1070,
    CliProgramExit = 1080,
    CliTextMessage = 1090,
    Compress = 1100,
    Crash = 1110,
    CreateKeyPair = 1120,
    DecryptTo = 1130,
    DecryptToFolder = 1140,
    DefaultInternal = 1150,
    DryRun = 1160,
    Echo = 1170,
    EncryptLike = 1175,
    EncryptTo = 1180,
    End = 1190,
    EnvironmentOption = 1200,
    ExportPublicKey = 1210,
    GplLicense = 1220,
    Help = 1230,
    Id = 1240,
    Internal = 1250,
    JwtAudience = 1260,
    JwtClaims = 1270,
    JwtCreateKeyPair = 1280,
    JwtIssuer = 1290,
    JwtPrivateKey = 1300,
    JwtSign = 1310,
    JwtVerify = 1320,
    LoadPrivateKeys = 1330,
    LoadPublicKeys = 1340,
    NoAsciiArmor = 1350,
    NoCompress = 1360,
    NoLog = 1370,
    NoOverwrite = 1380,
    NoProgress = 1390,
    OptionsCodeExport = 1400,
    OptionsFromFile = 1410,
    Overwrite = 1420,
    Password = 1430,
    Progressing = 1440,
    ProgressLog = 1450,
    Quiet = 1460,
    Slip39Combine = 1470,
    Slip39Group = 1480,
    Slip39GroupThreshold = 1490,
    Slip39Information = 1500,
    Slip39Password = 1510,
    Slip39Secret = 1520,
    Slip39Shares = 1530,
    Slip39Split = 1540,
    Stdout = 1550,
    TextLog = 1560,
    UseKeyPair = 1570,
    UsePublicKey = 1580,
    Wipe = 1590,
    WorkFolder = 1600,
    #endregion Possible-to-change values if API version is updated
}
