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

using AxCrypt.Core.UI;

using Xecrets.Cli.Log;

namespace Xecrets.Cli.Implementation;

/// <summary>
/// Wrap a progress context, to also count down progress for a accumulative totals counter.
/// </summary>
internal class TotalsProgressContext(IProgressContext progressToWrap, TotalsTracker totalsTracker) : IProgressContext
{
    public string Display { get { return progressToWrap.Display; } set { progressToWrap.Display = value; } }

    public bool Cancel { get { return progressToWrap.Cancel; } set { progressToWrap.Cancel = value; } }

    public bool AllItemsConfirmed { get { return progressToWrap.AllItemsConfirmed; } set { progressToWrap.AllItemsConfirmed = value; } }

    public ProgressTotals Totals { get => progressToWrap.Totals; }

    public event EventHandler<ProgressEventArgs>? Progressing
    {
        add { progressToWrap.Progressing += value; }
        remove { progressToWrap.Progressing -= value; }
    }

    /// <summary>
    /// Add to the count of work having been performed. May lead to a Progressing event.
    /// </summary>
    /// <param name="count">The amount of work having been performed in this step.</param>
    public void AddCount(long count)
    {
        totalsTracker.DoWork(count);
        progressToWrap.AddCount(count);
    }

    /// <summary>
    /// Add to the total work count.
    /// </summary>
    /// <param name="count">The amount of work to add.</param>
    public void AddTotal(long count)
    {
        progressToWrap.AddTotal(count);
    }

    public Task EnterSingleThread()
    {
        return progressToWrap.EnterSingleThread();
    }

    public void LeaveSingleThread()
    {
        progressToWrap.LeaveSingleThread();
    }

    public void NotifyLevelFinished()
    {
        totalsTracker.DoItems(1);
        progressToWrap.NotifyLevelFinished();
    }

    public void NotifyLevelStart()
    {
        progressToWrap.NotifyLevelStart();
    }

    public void RemoveCount(long totalCount, long progressCount)
    {
        progressToWrap.RemoveCount(totalCount, progressCount);
    }
}
