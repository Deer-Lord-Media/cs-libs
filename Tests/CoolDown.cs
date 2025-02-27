using CoolDown;
namespace Tests;

public class CoolDownTest
{
    [Fact]
    public void TestTracker()
    {
        var duration = new TimeSpan(0, 0, 0, 5, 0);
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration, "ID");
        coolDown.Start();
        Assert.InRange<TimeSpan>(coolDown.timeRemaining, new TimeSpan(1), duration);
        Assert.False(coolDown.IsReady);
        coolDown.Clear();
        Assert.True(coolDown.IsReady);
    }
}
