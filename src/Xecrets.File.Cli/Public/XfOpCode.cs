#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright 2022, Svante Seleborg, All Rights Reserved.
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
        CliOptionsCodeExport = 20,
        CliVersion = 30,
        CreateKeyPair = 40,
        DebugBreak = 50,
        DebugBreakParse = 60,
        DecryptTo = 70,
        DecryptToFolder = 80,
        DefaultInternal = 90,
        DryRun = 100,
        Echo = 110,
        EncryptTo = 120,
        EnvironmentOption = 130,
        Error = 140,
        ExportPublicKey = 150,
        GplLicense = 160,
        Help = 170,
        Id = 180,
        Internal = 190,
        JsonLog = 200,
        JwtAudience = 210,
        JwtClaims = 220,
        JwtCreateKeyPair = 230,
        JwtIssuer = 240,
        JwtPrivateKey = 250,
        JwtSign = 260,
        JwtVerify = 270,
        License = 280,
        LoadPublicKey = 290,
        NoLog = 300,
        NoOverwrite = 310,
        NoProgress = 320,
        OptionsFromFile = 330,
        Overwrite = 340,
        Password = 350,
        Platform = 360,
        ProgramExit = 370,
        Progressing = 380,
        ProgressLog = 390,
        Quiet = 400,
        Splash = 410,
        Stdout = 420,
        TextLog = 430,
        TextMessage = 440,
        UseKeyPair = 450,
        UsePublicKey = 460,
        Wipe = 470,
    }
}
