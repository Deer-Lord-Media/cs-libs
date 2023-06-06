using Buffs;
namespace Tests;

record VanillaData(int Id, TimeSpan Duration, int MaxStacks = 1) : Buffs.Data(Id, Duration, MaxStacks: MaxStacks)
{

}

public class TestContainer
{
    [Fact]
    public void SubclassData()
    {
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0, 0);
        VanillaData data = new VanillaData(1, duration);
    }

    [Fact]
    public void NoBuffs()
    {
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(16);
        Assert.Empty(buffs.Active);
    }

    
    [Fact]
    public void ApplyBuff()
    {
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        VanillaData buff = new VanillaData(1, duration);
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        Assert.Equal(buff, buffs.Active.First().Data);
    }

    [Fact]
    public void FullBuffs()
    {
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        foreach (int index in Enumerable.Range(1, 7))
        {
            VanillaData buff = new VanillaData(index, duration);
            buffs.Apply(buff);
        }
        Assert.Equal(4, buffs.Active.Count());
    }

    [Fact]
    public void StackBuff()
    {
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(4);
        TimeSpan duration = new TimeSpan(1, 0, 0, 0, 0, 0);
        VanillaData buff = new VanillaData(1, duration, 2);
        Assert.Equal(0, buffs.Active.Count());
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        var tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Stacks);
        buffs.Apply(buff);
        Assert.Equal(1, buffs.Active.Count());
        tracker = buffs.Active.First();
        Assert.Equal(2, tracker.Stacks);
    }
}
