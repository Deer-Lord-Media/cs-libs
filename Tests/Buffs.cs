using CSLibs.Buffs;

namespace Tests;

public class TestContainer
{
    [Fact]
    public void NoBuffs()
    {
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(16);
        Assert.Empty(buffs.Active);
    }

    
    [Fact]
    public void ApplyBuff()
    {
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        Data data = new Data(1, "NAME", duration);
        Buff? buff = buffs.Apply(data);
        Assert.NotNull(data);
        Assert.Single(buffs.Active);
        Assert.Equal(data, buffs.Active.First().Data);
    }

    [Fact]
    public void FullBuffs()
    {
        // ensure too many buffs can't be applied
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        foreach (int index in Enumerable.Range(1, 7))
        {
            Data buff = new Data(index, "NAME", duration);
            var success = buffs.Apply(buff);
        }
        Assert.Equal(4, buffs.Active.Count());
    }

    [Fact]
    public void SingleStackBuff()
    {
        // ensure single stack buffs can't overstack
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        Data buff = new Data(1, "NAME", duration);
        Assert.Equal(1, buff.MaxStacks);
        Assert.Empty(buffs.Active);
        buffs.Apply(buff);
        Assert.Single(buffs.Active);
        Assert.Equal(1, buffs.Active.First().Stacks);
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.First().Stacks);
    }

    [Fact]
    public void MultiStackBuff()
    {
        // ensure multi stack buffs can't overstack
        Buff tracker;
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        Data buff = new Data(1, "NAME", duration, maxStacks: 2);  
        Assert.Equal(2, buff.MaxStacks);
        Assert.Empty(buffs.Active);
        buffs.Apply(buff);
        Assert.Single(buffs.Active);
        tracker = buffs.Active.First();
        Assert.Equal(1, tracker.Stacks);
        buffs.Apply(buff);
        Assert.Single(buffs.Active);
        tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Stacks);
        buffs.Apply(buff);
        Assert.Single(buffs.Active);
        tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Stacks);
    }

    [Fact]
    public void BuffRefreshes()
    {
        // ensure re-application of an unexpired buff refreshes the duration and keeps stacks maximized
        Buff tracker;
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(0, 0, 20);
        TimeSpan wait = new TimeSpan(0, 0, 2);
        Data buff = new Data(1, "NAME", duration);
        buffs.Apply(buff);
        Thread.Sleep(wait);
        tracker = buffs.Active.First();
        TimeSpan timeLeft = tracker.TimeRemaining;
        Assert.True(tracker.TimeRemaining <= (duration - wait));
        buffs.Apply(buff);
        tracker = buffs.Active.First();
        Assert.True(tracker.TimeRemaining > timeLeft);
    }

    [Fact]
    public void BuffExpires()
    {
        // ensure buffs can be expired and not included
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(0, 0, 2);
        Data buff = new Data(1, "NAME", duration);
        buffs.Apply(buff);
        Assert.Single(buffs.Active);
        buffs.Expire();
        Assert.Single(buffs.Active);
        Thread.Sleep(duration);
        buffs.Expire();
        Assert.Empty(buffs.Active);
    }

    [Fact]
    public void BuffIsSimilarOne()
    {
        // ensure similar buffs can't be overridden
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, "NAME", duration, similarTo: new HashSet<int>{2,});
        Data buff2 = new Data(2, "NAME", duration);
        buffs.Apply(buff1);
        Assert.Single(buffs.Active);
        buffs.Apply(buff2);
        Assert.Single(buffs.Active);
        var tracker = buffs.Active.First();
        Assert.Equal(1, tracker.Data.Id);
    }

    [Fact]
    public void BuffIsSimilarTwo()
    {
        // checks similar from other side
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, "NAME", duration);
        Data buff2 = new Data(2, "NAME", duration, similarTo: new HashSet<int>{1,});
        buffs.Apply(buff1);
        Assert.Single(buffs.Active);
        buffs.Apply(buff2);
        Assert.Single(buffs.Active);
        var tracker = buffs.Active.First();
        Assert.Equal(1, tracker.Data.Id);
    }

    [Fact]
    public void BuffIsBlocked()
    {
        // buff2 cannot be applied, buff1 blocks it
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(1);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, "NAME", duration, precedenceOver: new HashSet<int>{2,});
        Data buff2 = new Data(2, "NAME", duration);
        buffs.Apply(buff1);
        Assert.Single(buffs.Active);
        buffs.Apply(buff2);
        Assert.Single(buffs.Active);
        var tracker = buffs.Active.First();
        Assert.Equal(1, tracker.Data.Id);
    }

    [Fact]
    public void BuffHasPrecedence()
    {
        // buff2 should push buff1 off
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(1);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, "NAME", duration);
        Data buff2 = new Data(2, "NAME", duration, precedenceOver: new HashSet<int>{1,});
        buffs.Apply(buff1);
        Assert.Single(buffs.Active);
        buffs.Apply(buff2);
        Assert.Single(buffs.Active);
        var tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Data.Id);
    }

    [Fact]
    public void BuffHasPriority()
    {
        // remove lower priority buff; more general than similarTo or precedenceOver
        CSLibs.Buffs.Tracker buffs = new CSLibs.Buffs.Tracker(1);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, "NAME", duration);
        Data buff2 = new Data(2, "NAME", duration, priorityLevel: 1);
        buffs.Apply(buff1);
        Assert.Single(buffs.Active);
        buffs.Apply(buff2);
        Assert.Single(buffs.Active);
        var tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Data.Id);
    }
}
