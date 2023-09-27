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
    /// confused by changes. The maximum code must be <= 127 to accomodate Linux/macOS
    /// restrictions.
    /// </summary>
    /// <remarks>
    /// Uses an 'Xf' prefix to distinguish itself, since its name is very generic.
    /// </remarks>
    public enum XfStatusCode
    {
        Success = 0,
        AxCryptException = 3,
        CannotDelete = 6,
        CannotRead = 9,
        CannotWrite = 12,
        CliToolIncompatible = 15,
        CliToolNotFound = 18,
        DebugBreakFailed = 21,
        Error = 24,
        ExceptionError = 27,
        ExtraArguments = 30,
        FileUnavailable = 33,
        InternalError = 36,
        InternalUnexpectedAction = 39,
        InternalUnknownStatus = 42,
        InvalidDays = 45,
        InvalidEmail = 48,
        InvalidLicenseFormat = 51,
        InvalidLicenseSignature = 54,
        InvalidOption = 57,
        InvalidPassword = 60,
        InvalidToken = 63,
        JwtDeserializeError = 66,
        LockedInUse = 69,
        MissingArgument = 72,
        NoDryRun = 75,
        NoPassword = 78,
        NotAFile = 81,
        NotAFolder = 84,
        NotSupported = 87,
        NotWritable = 90,
        NotYetImplemented = 93,
        PublicKeyNotFound = 96,
        UnhandledOperationException = 99,
        UnhandledRunException = 102,
        UnknownEnvironmentVariable = 105,
        UnknownOption = 108,
        Unlicensed = 111,
    }
}
