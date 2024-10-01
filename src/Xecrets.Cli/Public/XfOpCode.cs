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
    LoadPublicKey = 1320,
    NoAsciiArmor = 1330,
    NoCompress = 1340,
    NoLog = 1350,
    NoOverwrite = 1360,
    NoProgress = 1370,
    OptionsCodeExport = 1380,
    OptionsFromFile = 1390,
    Overwrite = 1400,
    Password = 1410,
    Progressing = 1420,
    ProgressLog = 1430,
    Quiet = 1440,
    Stdout = 1450,
    TextLog = 1460,
    UseKeyPair = 1470,
    UsePublicKey = 1480,
    Wipe = 1490
    #endregion Possible-to-change values if API version is updated
}
