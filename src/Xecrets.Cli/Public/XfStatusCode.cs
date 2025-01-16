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

    OsFileNotFound = 2, // 0x02
    OsPathNotFound = 3, // 0x03
    OsTooManyOpenFiles = 4, // 0x04
    OsAccessDenied = 5, // 0x05
    OsNotEnoughMemory = 8, // 0x08
    OsBadFormat = 11, // 0x0b
    OsInvalidData = 13, // 0x0d
    OsOutOfMemory = 14, // 0x0e
    OsNetOutOfMemory = 23, // 0x17
    OsIllegalInstruction = 29, // 0x1d
    OsSharingViolation = 32, // 0x20
    OsLockViolation = 33, // 0x21
    OsBadImageFormat = 123, // 0x7b
    OsStackOverflow = 253, // 0xfd

    AxCryptException = 6,
    BadSequence = 9,
    Canceled = 12,
    CannotDelete = 15,
    CannotRead = 18,
    CannotWrite = 21,
    CliToolIncompatible = 24,
    CliToolNotFound = 27,
    DebugBreakFailed = 30,
    DeserializeError = 36,
    Error = 39,
    ExceptionError = 42,
    ExtraArguments = 45,
    FileUnavailable = 48,
    InternalError = 51,
    InternalUnexpectedAction = 54,
    InternalUnknownStatus = 57,
    InvalidDays = 60,
    InvalidEmail = 63,
    InvalidLicenseFormat = 66,
    InvalidLicenseSignature = 69,
    InvalidOption = 72,
    InvalidPassword = 75,
    InvalidToken = 78,
    LockedInUse = 81,
    MissingArgument = 84,
    NoDryRun = 87,
    NoPassword = 90,
    NotAFile = 93,
    NotAFolder = 96,
    NotSupported = 99,
    NotWritable = 102,
    NotYetImplemented = 105,
    PublicKeyNotFound = 108,
    Slip39Error = 111,
    UnhandledOperationException = 114,
    UnhandledRunException = 117,
    UnknownEnvironmentVariable = 120,
    UnknownOption = 126,
    Unlicensed = 127,
}
