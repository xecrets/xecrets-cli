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

using AxCrypt.Abstractions;
using AxCrypt.Core.Runtime;
using AxCrypt.Fake;

using NUnit.Framework;

using Xecrets.Cli.Implementation;
using Xecrets.Cli.Log;
using Xecrets.Core;
using Xecrets.Core.Models;

namespace Xecrets.Cli.Test;

[TestFixture]
internal class TestTotalsProgressContext
{
    [SetUp]
    public void SetUp()
    {
        TypeMap.Register.Singleton<IRuntimeEnvironment>(() => new FakeRuntimeEnvironment());
        TypeMap.Register.Singleton<IUIThread>(() => new FakeUIThread());
    }

    [Test]
    public void CoreProgressUpdatesTotalsTrackerAndRaisesIntermediateProgress()
    {
        using CancelSignal cancelSignal = new();
        CliServices cliServices = new(
            coreServices: null!,
            timeProvider: TimeProvider.System,
            cancelSignal: cancelSignal,
            consoleOut: new ConsoleOut(TextWriter.Null),
            splash: null!,
            buildUtc: null!,
            license: null!,
            licenseCandidates: null!,
            licenseLocator: null!,
            desktopServices: null!,
            inUseBy: null!,
            shamirsSecretSharing: null!);
        TotalsTracker totalsTracker = new(cliServices);
        TotalsProgressContext progressContext = new(new NoProgressContext(TimeSpan.Zero, TimeSpan.Zero), totalsTracker)
        {
            Display = "work-item",
        };

        List<int> percents = [];
        progressContext.Progressing += (_, e) => percents.Add(e.Percent);

        progressContext.Report(Progress.LevelStarted());
        progressContext.Report(Progress.TotalAdded(10));
        progressContext.Report(Progress.CountAdded(5));
        progressContext.Report(Progress.LevelFinished());

        Assert.That(percents, Is.EqualTo([50, 100]));
        Assert.That(totalsTracker.TotalDone, Is.EqualTo(5));
        Assert.That(totalsTracker.ItemsDone, Is.EqualTo(1));
    }
}
