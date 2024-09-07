﻿#region Copyright and GPL License

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

using System.Runtime.Serialization;

using Xecrets.Cli.Public;

namespace Xecrets.Cli
{
    internal class XecretsCliException : Exception
    {
        public Status Status { get; }

        public XecretsCliException()
        {
            Status = new Status(XfStatusCode.ExceptionError, "An exception occurred.");
        }
        public XecretsCliException(Status status)
            : this(status.Message)
        {
            Status = status;
        }

        public XecretsCliException(string message)
            : base(message)
        {
            Status = new Status(XfStatusCode.ExceptionError, message);
        }

        public XecretsCliException(string message, Exception inner)
            : base(message, inner)
        {
            Status = new Status(XfStatusCode.ExceptionError, message);
        }
    }
}
