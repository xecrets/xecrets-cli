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
    BadSequence = 6,
    CannotDelete = 9,
    CannotRead = 12,
    CannotWrite = 15,
    CliToolIncompatible = 18,
    CliToolNotFound = 21,
    DebugBreakFailed = 24,
    Error = 27,
    ExceptionError = 30,
    ExtraArguments = 33,
    FileUnavailable = 36,
    InternalError = 39,
    InternalUnexpectedAction = 42,
    InternalUnknownStatus = 45,
    InvalidDays = 48,
    InvalidEmail = 51,
    InvalidLicenseFormat = 54,
    InvalidLicenseSignature = 57,
    InvalidOption = 60,
    InvalidPassword = 63,
    InvalidToken = 66,
    JwtDeserializeError = 69,
    LockedInUse = 72,
    MissingArgument = 75,
    NoDryRun = 78,
    NoPassword = 81,
    NotAFile = 84,
    NotAFolder = 87,
    NotSupported = 90,
    NotWritable = 93,
    NotYetImplemented = 96,
    PublicKeyNotFound = 99,
    UnhandledOperationException = 102,
    UnhandledRunException = 105,
    UnknownEnvironmentVariable = 108,
    UnknownOption = 111,
    Unlicensed = 114,
}
