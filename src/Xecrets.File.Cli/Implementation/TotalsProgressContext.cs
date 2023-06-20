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

using AxCrypt.Core.UI;

using Xecrets.File.Cli.Log;

namespace Xecrets.File.Cli.Implementation
{
    /// <summary>
    /// Wrap a progress context, to also count down progress for a accumulative totals counter.
    /// </summary>
    internal class TotalsProgressContext : IProgressContext
    {
        private readonly IProgressContext _progressToWrap;

        private readonly TotalsTracker _totalsTracker;

        public TotalsProgressContext(IProgressContext progressToWrap, TotalsTracker totalsTracker)
        {
            _progressToWrap = progressToWrap;
            _totalsTracker = totalsTracker;
        }

        public string Display { get { return _progressToWrap.Display; } set { _progressToWrap.Display = value; } }

        public bool Cancel { get { return _progressToWrap.Cancel; } set { _progressToWrap.Cancel = value; } }

        public bool AllItemsConfirmed { get { return _progressToWrap.AllItemsConfirmed; } set { _progressToWrap.AllItemsConfirmed = value; } }

        public ProgressTotals Totals { get => _progressToWrap.Totals; }

        public event EventHandler<ProgressEventArgs>? Progressing
        {
            add { _progressToWrap.Progressing += value; }
            remove { _progressToWrap.Progressing -= value; }
        }

        /// <summary>
        /// Add to the count of work having been performed. May lead to a Progressing event.
        /// </summary>
        /// <param name="count">The amount of work having been performed in this step.</param>
        public void AddCount(long count)
        {
            _totalsTracker.DoWork(count);
            _progressToWrap.AddCount(count);
        }

        /// <summary>
        /// Add to the total work count.
        /// </summary>
        /// <param name="count">The amount of work to add.</param>
        public void AddTotal(long count)
        {
            _progressToWrap.AddTotal(count);
        }

        public Task EnterSingleThread()
        {
            return _progressToWrap.EnterSingleThread();
        }

        public void LeaveSingleThread()
        {
            _progressToWrap.LeaveSingleThread();
        }

        public void NotifyLevelFinished()
        {
            _totalsTracker.DoItems(1);
            _progressToWrap.NotifyLevelFinished();
        }

        public void NotifyLevelStart()
        {
            _progressToWrap.NotifyLevelStart();
        }

        public void RemoveCount(long totalCount, long progressCount)
        {
            _progressToWrap.RemoveCount(totalCount, progressCount);
        }
    }
}
