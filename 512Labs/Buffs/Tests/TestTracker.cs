using Buffs;
namespace Tests;

public class TestContainer
{
    [Fact]
    public void NoBuffs()
    {
        Buffs.Tracker buffs = new Buffs.Tracker(16);
        Assert.Empty(buffs.Active);
    }

    
    [Fact]
    public void ApplyBuff()
    {
        Buffs.Tracker buffs = new Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        Data buff = new Data(1, duration);
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        Assert.Equal(buff, buffs.Active.First().Data);
    }

    [Fact]
    public void FullBuffs()
    {
        // ensure too many buffs can't be applied
        Buffs.Tracker buffs = new Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        foreach (int index in Enumerable.Range(1, 7))
        {
            Data buff = new Data(index, duration);
            buffs.Apply(buff);
        }
        Assert.Equal(4, buffs.Active.Count());
    }

    [Fact]
    public void SingleStackBuff()
    {
        // ensure single stack buffs can't overstack
        Buffs.Tracker buffs = new Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        Data buff = new Data(1, duration);  
        Assert.Equal(1, buff.MaxStacks);
        Assert.Equal(0, buffs.Active.Count());
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        Assert.Equal(1, buffs.Active.First().Stacks);
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.First().Stacks);
    }

    [Fact]
    public void MultiStackBuff()
    {
        // ensure multi stack buffs can't overstack
        Buff tracker;
        Buffs.Tracker buffs = new Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        Data buff = new Data(1, duration, MaxStacks: 2);  
        Assert.Equal(2, buff.MaxStacks);
        Assert.Equal(0, buffs.Active.Count());
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        tracker = buffs.Active.First();
        Assert.Equal(1, tracker.Stacks);
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Stacks);
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Stacks);
    }

    [Fact]
    public void BuffRefreshes()
    {
        // ensure re-application of an unexpired buff refreshes the duration and keeps stacks maximized
        Buff tracker;
        Buffs.Tracker buffs = new Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(0, 0, 20);
        TimeSpan wait = new TimeSpan(0, 0, 2);
        Data buff = new Data(1, duration);
        buffs.Apply(buff);
        Thread.Sleep(wait);
        tracker = buffs.Active.First();
        TimeSpan timeLeft = tracker.TimeLeft;
        Assert.True(tracker.TimeLeft <= (duration - wait));
        buffs.Apply(buff);
        tracker = buffs.Active.First();
        Assert.True(tracker.TimeLeft > timeLeft);
    }

    [Fact]
    public void BuffExpires()
    {
        // ensure buffs can be expired and not included
        Buffs.Tracker buffs = new Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(0, 0, 2);
        Data buff = new Data(1, duration);
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        buffs.Expire();
        Assert.Equal(1, buffs.Active.Count());
        Thread.Sleep(duration);
        buffs.Expire();
        Assert.Equal(0, buffs.Active.Count());
    }

    [Fact]
    public void BuffIsSimilarOne()
    {
        // ensure similar buffs can't be overridden
        Buffs.Tracker buffs = new Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, duration, SimilarTo: new HashSet<int>{2,});
        Data buff2 = new Data(2, duration);
        buffs.Apply(buff1);
        Assert.Equal(1, buffs.Active.Count());
        buffs.Apply(buff2);
        Assert.Equal(1, buffs.Active.Count());
        var tracker = buffs.Active.First();
        Assert.Equal(1, tracker.Data.Id);
    }

    [Fact]
    public void BuffIsSimilarTwo()
    {
        // checks similar from other side
        Buffs.Tracker buffs = new Buffs.Tracker(4);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, duration);
        Data buff2 = new Data(2, duration, SimilarTo: new HashSet<int>{1,});
        buffs.Apply(buff1);
        Assert.Equal(1, buffs.Active.Count());
        buffs.Apply(buff2);
        Assert.Equal(1, buffs.Active.Count());
        var tracker = buffs.Active.First();
        Assert.Equal(1, tracker.Data.Id);
    }

    [Fact]
    public void BuffIsBlocked()
    {
        // buff2 cannot be applied, buff1 blocks it
        Buffs.Tracker buffs = new Buffs.Tracker(1);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, duration, PrecedenceOver: new HashSet<int>{2,});
        Data buff2 = new Data(2, duration);
        buffs.Apply(buff1);
        Assert.Equal(1, buffs.Active.Count());
        buffs.Apply(buff2);
        Assert.Equal(1, buffs.Active.Count());
        var tracker = buffs.Active.First();
        Assert.Equal(1, tracker.Data.Id);
    }

    [Fact]
    public void BuffHasPrecedence()
    {
        // buff2 should push buff1 off
        Buffs.Tracker buffs = new Buffs.Tracker(1);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, duration);
        Data buff2 = new Data(2, duration, PrecedenceOver: new HashSet<int>{1,});
        buffs.Apply(buff1);
        Assert.Equal(1, buffs.Active.Count());
        buffs.Apply(buff2);
        Assert.Equal(1, buffs.Active.Count());
        var tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Data.Id);
    }

    [Fact]
    public void BuffHasPriority()
    {
        // remove lower priority buff; more general than similarTo or precedenceOver
        Buffs.Tracker buffs = new Buffs.Tracker(1);
        TimeSpan duration = new TimeSpan(0, 0, 100);
        Data buff1 = new Data(1, duration);
        Data buff2 = new Data(2, duration, PriorityLevel: 1);
        buffs.Apply(buff1);
        Assert.Equal(1, buffs.Active.Count());
        buffs.Apply(buff2);
        Assert.Equal(1, buffs.Active.Count());
        var tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Data.Id);
    }
}
