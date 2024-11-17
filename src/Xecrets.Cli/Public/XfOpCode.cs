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
    CreateKeyPair = 1110,
    DecryptTo = 1120,
    DecryptToFolder = 1130,
    DefaultInternal = 1140,
    DryRun = 1150,
    Echo = 1160,
    EncryptTo = 1170,
    End = 1180,
    EnvironmentOption = 1190,
    ExportPublicKey = 1200,
    GplLicense = 1210,
    Help = 1220,
    Id = 1230,
    Internal = 1240,
    JwtAudience = 1250,
    JwtClaims = 1260,
    JwtCreateKeyPair = 1270,
    JwtIssuer = 1280,
    JwtPrivateKey = 1290,
    JwtSign = 1300,
    JwtVerify = 1310,
    LoadPrivateKeys = 1320,
    LoadPublicKeys = 1330,
    NoAsciiArmor = 1340,
    NoCompress = 1350,
    NoLog = 1360,
    NoOverwrite = 1370,
    NoProgress = 1380,
    OptionsCodeExport = 1390,
    OptionsFromFile = 1400,
    Overwrite = 1410,
    Password = 1420,
    Progressing = 1430,
    ProgressLog = 1440,
    Quiet = 1450,
    Slip39Combine = 1460,
    Slip39Group = 1470,
    Slip39GroupThreshold = 1480,
    Slip39Information = 1490,
    Slip39Password = 1500,
    Slip39Secret = 1510,
    Slip39Shares = 1520,
    Slip39Split = 1530,
    Stdout = 1540,
    TextLog = 1550,
    UseKeyPair = 1560,
    UsePublicKey = 1570,
    Wipe = 1580
    #endregion Possible-to-change values if API version is updated
}
