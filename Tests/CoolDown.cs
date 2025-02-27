using CoolDown;
namespace Tests;

public class CoolDownTest
{
    [Fact]
    public void TestTracker()
    {
        var duration = new TimeSpan(0, 0, 0, 5, 0);
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration, "ID");
        var started = coolDown.Start();
        Assert.True(started);
        Assert.InRange<TimeSpan>(coolDown.timeRemaining, new TimeSpan(1), duration);
        Assert.False(coolDown.IsReady);
        var cleared = coolDown.Clear();
        Assert.True(cleared);
        Assert.True(coolDown.IsReady);
    }
}
