#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright 2023, Svante Seleborg, All Rights Reserved.
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
        DebugBreakFailed = 140,
        Error = 150,
        ExceptionError = 160,
        ExtraArguments = 170,
        FileUnavailable = 180,
        InternalError = 190,
        InternalUnexpectedAction = 200,
        InternalUnknownStatus = 210,
        InvalidDays = 220,
        InvalidEmail = 230,
        InvalidLicenseFormat = 240,
        InvalidLicenseSignature = 250,
        InvalidOption = 260,
        InvalidPassword = 270,
        InvalidToken = 280,
        JwtDeserializeError = 290,
        LockedInUse = 300,
        MissingArgument = 310,
        NoDryRun = 320,
        NoPassword = 330,
        NotAFile = 340,
        NotAFolder = 350,
        NotSupported = 360,
        NotWritable = 370,
        NotYetImplemented = 380,
        PublicKeyNotFound = 390,
        UnhandledOperationException = 400,
        UnhandledRunException = 410,
        UnknownEnvironmentVariable = 420,
        UnknownOption = 430,
        Unlicensed = 440,
    }
}
