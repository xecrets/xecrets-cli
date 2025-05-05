#region Copyright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2025 Svante Seleborg, All Rights Reserved.
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
/// Sub-status codes giving a more detailed view of the status of an operation.
/// </summary>
internal enum XfSubStatusCode
{
    /// <summary>
    /// Default and indicating sucess.
    /// </summary>
    Success = 0,

    /// <summary>
    /// An unknown error occurred.
    /// </summary>
    Unknown = 1,

    /// <summary>
    /// A checksum or digest was found to be incorrect. Check the exception message for details.
    /// </summary>
    Slip39InvalidChecksum = 1010,

    /// <summary>
    /// Group specification or actual group meta data is invalid. Check the exception message for details.
    /// </summary>
    Slip39InvalidGroups = 1020,

    /// <summary>
    /// The number of shares is insufficient. Check the exception message for details.
    /// </summary>
    Slip39InsufficientShares = 1030,

    /// <summary>
    /// The set of shares have inconsistent meta data. Check the exception message for details.
    /// </summary>
    Slip39InconsistentShares = 1040,

    /// <summary>
    /// Input is in the wrong format. Check the exception message for details.
    /// </summary>
    Slip39InvalidFormat = 1050,

    /// <summary>
    /// An invalid mnemonic word or set of words were input. Check the exception message for details.
    /// </summary>
    Slip39InvalidMnemonic = 1060,

    /// <summary>
    /// A file to decrypt was found to be empty. This is not a valid file to decrypt.
    /// </summary>
    ZeroLengthFile = 1070,

    /// <summary>
    /// A file to decrypt does not have the correct magic GUID. This probably means it's not an encrypted file.
    /// </summary>
    InvalidMagicGuid = 1080,
}
