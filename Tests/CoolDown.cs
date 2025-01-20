using CoolDown;
namespace Tests;

public class CoolDownTest
{
    [Fact]
    public void Test1()
    {
        CoolDown.Tracker coolDown = new CoolDown.Tracker(new TimeSpan(1));
    }
}
