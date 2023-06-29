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
    /// Status codes for messages and also exit codes. They must be fixed since
    /// external programs may need to interpret them correctly and not be
    /// confused by changes.
    /// </summary>
    /// <remarks>
    /// Uses an 'Xf' prefix to distinguish itself, since its name is very generic.
    /// </remarks>
    public enum XfStatusCode
    {
        Success = 0,
        AxCryptException = 100,
        CannotDelete = 110,
        CannotRead = 120,
        CannotWrite = 130,
        CliToolIncompatible = 140,
        CliToolNotFound = 150,
        DebugBreakFailed = 160,
        Error = 170,
        ExceptionError = 180,
        ExtraArguments = 190,
        FileUnavailable = 200,
        InternalError = 210,
        InternalUnexpectedAction = 220,
        InternalUnknownStatus = 230,
        InvalidDays = 240,
        InvalidEmail = 250,
        InvalidLicenseFormat = 260,
        InvalidLicenseSignature = 270,
        InvalidOption = 280,
        InvalidPassword = 290,
        InvalidToken = 300,
        JwtDeserializeError = 310,
        LockedInUse = 320,
        MissingArgument = 330,
        NoDryRun = 340,
        NoPassword = 350,
        NotAFile = 360,
        NotAFolder = 370,
        NotSupported = 380,
        NotWritable = 390,
        NotYetImplemented = 400,
        PublicKeyNotFound = 410,
        UnhandledOperationException = 420,
        UnhandledRunException = 430,
        UnknownEnvironmentVariable = 440,
        UnknownOption = 450,
        Unlicensed = 460,
    }
}
