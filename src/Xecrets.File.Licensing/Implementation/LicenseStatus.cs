#region Coypright and GPL License

/*
 * Xecrets File Licensing - Copyright 2023, Svante Seleborg, All Rights Reserved.
 *
 * This code file may be used by Xecrets File Cli, parts of which in turn are derived from AxCrypt as licensed under GPL v3 or later.
 * 
 * However, this code is not derived from AxCrypt and is separately copyrighted and only licensed as follows unless
 * explicitly licensed otherwise. If you use any part of this code in your software, please see https://www.gnu.org/licenses/
 * for details of what this means for you.
 *
 * Xecrets File Licensing is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * Xecrets File Licensing is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with Xecrets File Cli.  If not, see <https://www.gnu.org/licenses/>.
 *
 * The source repository can be found at https://github.com/ please go there for more information, suggestions and
 * contributions. You may also visit https://www.axantum.com for more information about the author.
*/

#endregion Coypright and GPL License

namespace Xecrets.File.Licensing.Abstractions
{
    public enum LicenseStatus
    {
        None = 0,
        /// <summary>
        /// It's a GPL build and download subscription licensing is irrelevant.
        /// Splash blurb politely and concisely asks to get a download subscription.
        /// </summary>
        Gpl = 1,

        /// <summary>
        /// It's an Axantum build, but there is no subscription license at all.
        /// Splash blurb is longer and explains the specific situation.
        /// Forceably waits for user input and delays 10 seconds unless stdin and stdout is redirected and it's Json.
        /// </summary>
        Unlicensed = 2,

        /// <summary>
        /// It's an Axantum build, but the subscription license is not valid for this version
        /// because it's too new.
        /// Splash blurb is longer and explains the specific situation.
        /// Forceably waits for user input and delays 10 seconds unless stdin and stdout is redirected and it's Json.
        /// </summary>
        Expired = 3,

        /// <summary>
        /// It's an Axantum build, and the subscription license is valid for this version.
        /// Splash blurb politely and concisely thanks for the contribution.
        /// </summary>
        Valid = 4,

        /// <summary>
        /// It's an Axantum build, but the subscription while not expired is not valid for
        /// this software.
        /// Splash blurb is longer and explains the specific situation.
        /// Forceably waits for user input and delays 10 seconds unless stdin and stdout is redirected and it's Json.
        /// </summary>
        Invalid = 5,
    }
}
