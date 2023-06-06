using Buffs;
namespace Tests;

public class TestContainer
{
    [Fact]
    public void SubclassData()
    {
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0, 0);
        Data data = new Data(1, duration);
    }

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
    public void SingleStack()
    {
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
    public void MultiStack()
    {
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

    }

    [Fact]
    public void BuffExpires()
    {

    }

    [Fact]
    public void BuffIsSimilar()
    {

    }

    [Fact]
    public void BuffIsBlocked()
    {

    } 
}
