using Buffs;
namespace Tests;

record VanillaData(int Id, TimeSpan Duration) : Buffs.Data(Id, Duration)
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
    public void EmptyContainer()
    {
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(16);
        Assert.Empty(buffs.Active);
    }

    [Fact]
    public void AddBuffs()
    {
        int maxBuffs = 16;
        var ids = Enumerable.Range(1, 20);
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 2, 0);
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(maxBuffs);
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
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(maxBuffs);
        foreach (int id in ids)
        {
            VanillaData data = new VanillaData(id, duration);
            buffs.Add(data);
            var removed = buffs.Remove(data);
        }
        Assert.Empty(buffs.Active);
    }

    [Fact]
    public void ActiveBuffs()
    {
        int maxBuffs = 16;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0, -1);
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(maxBuffs);
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
        Buffs.Collection<VanillaData> buffs = new Buffs.Collection<VanillaData>(maxBuffs);
        Assert.Empty(buffs.Active);
        var data1 = new VanillaData(1, new TimeSpan(0, 0, 0, 1, 0, 0));
        var data2 = new VanillaData(2, new TimeSpan(0, 0, 0, 1, 0, 0));
        buffs.Add(data1);
        Assert.True(buffs.Contains(data1));
        Assert.False(buffs.Contains(data2));
    }
}
