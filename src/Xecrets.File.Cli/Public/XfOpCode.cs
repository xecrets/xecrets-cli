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
        None = 0,
        ArgumentMarkdown = 10,
        CliCrashLog = 20,
        CliLicense = 30,
        CliOptionsCodeExport = 40,
        CliVersion = 50,
        CreateKeyPair = 60,
        DebugBreak = 70,
        DebugBreakParse = 80,
        DecryptTo = 90,
        DecryptToFolder = 100,
        DefaultInternal = 110,
        DryRun = 120,
        Echo = 130,
        EncryptTo = 140,
        EnvironmentOption = 150,
        Error = 160,
        ExportPublicKey = 170,
        GplLicense = 180,
        Help = 190,
        Id = 200,
        Internal = 210,
        JsonLog = 220,
        JwtAudience = 230,
        JwtClaims = 240,
        JwtCreateKeyPair = 250,
        JwtIssuer = 260,
        JwtPrivateKey = 270,
        JwtSign = 280,
        JwtVerify = 290,
        LoadPublicKey = 300,
        NoLog = 310,
        NoOverwrite = 320,
        NoProgress = 330,
        OptionsFromFile = 340,
        Overwrite = 350,
        Password = 360,
        Platform = 370,
        ProgramExit = 380,
        Progressing = 390,
        ProgressLog = 400,
        Quiet = 410,
        Splash = 420,
        Stdout = 430,
        TextLog = 440,
        TextMessage = 450,
        UseKeyPair = 460,
        UsePublicKey = 470,
        Wipe = 480,
    }
}
