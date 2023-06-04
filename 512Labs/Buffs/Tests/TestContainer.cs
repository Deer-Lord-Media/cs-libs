using Buffs;
namespace Tests;

record VanillaData : Buffs.Data
{
    public VanillaData(int id, TimeSpan duration)
    {
        this.Id = id;
        this.Duration = duration;
    }
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
    public void EmptyContainer()
    {
        Buffs.Container<VanillaData> buffs = new Buffs.Container<VanillaData>(16);
        Assert.Empty(buffs.Active);
    }

    [Fact]
    public void AddBuffs()
    {
        int maxBuffs = 16;
        var ids = Enumerable.Range(1, 20);
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 2, 0);
        Buffs.Container<VanillaData> buffs = new Buffs.Container<VanillaData>(maxBuffs);
        foreach (int id in ids)
        {
            VanillaData data = new VanillaData(id, duration);
            buffs.Add(data);
        }
        Assert.Equal(maxBuffs, buffs.Active.Count());
    }

    [Fact]
    public void RemoveBuffs()
    {
        int maxBuffs = 16;
        var ids = Enumerable.Range(1, 20);
        TimeSpan duration = new TimeSpan(0, 0, 0, 1, 0, 0);
        Buffs.Container<VanillaData> buffs = new Buffs.Container<VanillaData>(maxBuffs);
        foreach (int id in ids)
        {
            VanillaData data = new VanillaData(id, duration);
            var added = buffs.Add(data);
            Assert.True(added);
            var removed = buffs.Remove(data);
            Assert.True(removed);
        }
        Assert.Empty(buffs.Active);
    }

    [Fact]
    public void ActiveBuffs()
    {
        int maxBuffs = 16;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0, -1);
        Buffs.Container<VanillaData> buffs = new Buffs.Container<VanillaData>(maxBuffs);
        Assert.Empty(buffs.Active);
        var data1 = new VanillaData(1, new TimeSpan(0, 0, 0, -1, 0, 0));
        var data2 = new VanillaData(2, new TimeSpan(0, 0, 0, 1, 0, 0));
        buffs.Add(data1);
        buffs.Add(data2);
        Assert.Single(buffs.Active);
    }

    [Fact]
    public void CheckForBuff()
    {
        int maxBuffs = 16;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0, -1);
        Buffs.Container<VanillaData> buffs = new Buffs.Container<VanillaData>(maxBuffs);
        Assert.Empty(buffs.Active);
        var data1 = new VanillaData(1, new TimeSpan(0, 0, 0, 1, 0, 0));
        var data2 = new VanillaData(2, new TimeSpan(0, 0, 0, 1, 0, 0));
        buffs.Add(data1);
        Assert.True(buffs.Contains(data1));
        Assert.False(buffs.Contains(data2));
    }
}
