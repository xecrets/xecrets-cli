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
        ExtraArguments = 160,
        FileUnavailable = 170,
        InternalError = 180,
        InternalUnexpectedAction = 190,
        InternalUnknownStatus = 200,
        InvalidDays = 210,
        InvalidEmail = 220,
        InvalidLicenseFormat = 230,
        InvalidLicenseSignature = 240,
        InvalidOption = 250,
        InvalidPassword = 260,
        InvalidToken = 270,
        LockedInUse = 280,
        MissingArgument = 290,
        NoDryRun = 300,
        NoPassword = 310,
        NotAFile = 320,
        NotAFolder = 330,
        NotSupported = 340,
        NotWritable = 350,
        NotYetImplemented = 360,
        PublicKeyNotFound = 370,
        UnknownEnvironmentVariable = 380,
        UnknownOption = 390,
        Unlicensed = 490,
    }
}
