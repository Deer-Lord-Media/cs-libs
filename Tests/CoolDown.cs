using CoolDown;
namespace Tests;

public class CoolDownTest
{
    [Fact]
    public void TestTracker()
    {
        var duration = new TimeSpan(0, 0, 0, 5, 0);
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration);
        var started = coolDown.Start();
        Assert.True(started);
        Assert.InRange<TimeSpan>(coolDown.TimeRemaining, new TimeSpan(1), duration);
        Assert.False(coolDown.IsReady);
        var reset = coolDown.Reset();
        Assert.True(reset);
        Assert.True(coolDown.IsReady);
    }
}
