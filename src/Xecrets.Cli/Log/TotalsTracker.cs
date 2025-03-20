﻿#region Copyright and GPL License

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

using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Log;

internal class TotalsTracker
{
    private const LogStyle DefaultTextLogStyle = LogStyle.Text | LogStyle.Progress;

    private const LogStyle DefaultJsonLogStyle = LogStyle.Json | LogStyle.Progress;

    public LogStyle LogStyle { get; set; } = DefaultTextLogStyle;

    public int ItemsTotal { get; private set; }

    public int ItemsDone { get; private set; }

    public long TotalWork { get; private set; }

    public long TotalDone { get; private set; }

    public ILogger Logger { get; private set; }

    public string Id { get; set; } = string.Empty;

    private readonly CancelSignal _cancelSignal;

    public TotalsTracker()
    {
        _cancelSignal = New<CancelSignal>();
        Logger = GetCurrentLogger();
    }

    public void ResetLogger()
    {
        Logger = GetCurrentLogger();
    }

    public void ResetLogger(Parameters parameters)
    {
        LogStyle = (parameters.Parser.ParsedOps.FirstOrDefault()?.OpCode ?? XfOpCode.None) == XfOpCode.SdkJsonLog
            ? DefaultJsonLogStyle : DefaultTextLogStyle;
        if (parameters.Parser.IsQuiet)
        {
            LogStyle = LogStyle.None;
        }
        ResetLogger();
    }

    private ILogger GetCurrentLogger()
    {
        return LogStyle switch
        {
            LogStyle.Json or LogStyle.Json | LogStyle.Progress => new JsonLogger(this, progress: LogStyle.HasFlag(LogStyle.Progress)),
            LogStyle.Text or LogStyle.Text | LogStyle.Progress => new TextLogger(this, progress: LogStyle.HasFlag(LogStyle.Progress)),
            LogStyle.None or LogStyle.Progress => new NoLogger(this),
            _ => throw new ArgumentException("Unexpected LogStyle value.", nameof(LogStyle)),
        };
    }

    public void AddWorkItem(long count)
    {
        AddWork(count);
        ItemsTotal += 1;
        AssertNoCancel();
    }

    public void AddWork(long count)
    {
        TotalWork += count;
        AssertNoCancel();
    }

    public void DoWork(long count)
    {
        TotalDone += count;
        AssertNoCancel();
    }

    public void DoItems(int items)
    {
        ItemsDone += items;
        AssertNoCancel();
    }

    private void AssertNoCancel()
    {
        if (_cancelSignal.ImmediateToken.IsCancellationRequested)
        {
            throw new OperationCanceledException("Operation immediately canceled.");
        }
    }
}
