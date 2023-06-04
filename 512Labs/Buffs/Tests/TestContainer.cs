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
}
