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

namespace Xecrets.Cli.Public
{
    /// <summary>
    /// The version must be updated when the command line options are updated in
    /// an incompatible way. Increase the minor version, if changes do not
    /// change the meaning or syntax of any pre-existing options, i.e. purely
    /// new additional capabilities. Increase the major version, if changes in
    /// any way changes an existing option so it may not work as expected by an
    /// older consumer. The consumer should thus only accept an export version
    /// that has the same Major version, and a minor version greather than or equal
    /// to it's known version.
    /// </summary>
    public static class XfExportVersion
    {
        public static Version CliVersion => new(2, 0);
    }
}
