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
    Canceled = 9,
    CannotDelete = 12,
    CannotRead = 15,
    CannotWrite = 18,
    CliToolIncompatible = 21,
    CliToolNotFound = 24,
    DebugBreakFailed = 27,
    DeserializeError = 30,
    Error = 33,
    ExceptionError = 36,
    ExtraArguments = 39,
    FileUnavailable = 42,
    InternalError = 45,
    InternalUnexpectedAction = 48,
    InternalUnknownStatus = 51,
    InvalidDays = 54,
    InvalidEmail = 57,
    InvalidLicenseFormat = 60,
    InvalidLicenseSignature = 63,
    InvalidOption = 66,
    InvalidPassword = 69,
    InvalidToken = 72,
    LockedInUse = 75,
    MissingArgument = 78,
    NoDryRun = 81,
    NoPassword = 84,
    NotAFile = 87,
    NotAFolder = 90,
    NotSupported = 93,
    NotWritable = 96,
    NotYetImplemented = 99,
    PublicKeyNotFound = 102,
    Slip39Error = 105,
    UnhandledOperationException = 108,
    UnhandledRunException = 111,
    UnknownEnvironmentVariable = 114,
    UnknownOption = 117,
    Unlicensed = 120,
}
