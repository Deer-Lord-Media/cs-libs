using System.IO.Compression;
using System.Reflection.Metadata;
using CoolDown;
namespace Tests;

public class CoolDownTest
{
    [Fact]
    public void TestOperations()
    {
        var duration = new TimeSpan(0, 0, 0, 1, 0);
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration, "ID");

        void HandleStart(object? sender, Message e)
        {
            Assert.Equal<CoolDown.Action>(CoolDown.Action.Start, e.Action);
            Assert.Equal("ID", e.Id);
        }
        coolDown.Register(HandleStart);
        var started = coolDown.Start();
        Assert.True(started);
        coolDown.Deregister(HandleStart);
        Assert.InRange<TimeSpan>(coolDown.TimeRemaining, new TimeSpan(1), duration);

        void HandleRefresh(object? sender, Message e)
        {
            Assert.Equal<CoolDown.Action>(CoolDown.Action.Refresh, e.Action);
            Assert.Equal("ID", e.Id);
        }
        coolDown.Register(HandleRefresh);

        var refreshed = coolDown.Refresh();
        Assert.True(refreshed);
        coolDown.Deregister(HandleRefresh);

        void HandleClear(object? sender, Message e)
        {
            Assert.Equal<CoolDown.Action>(CoolDown.Action.Expire, e.Action);
            Assert.Equal("ID", e.Id);

        }
        coolDown.Register(HandleClear);
        var cleared = coolDown.Clear();
        Assert.True(cleared);
        coolDown.Deregister(HandleClear);
    }

    [Fact]
    public void TestExpiry()
    {
        var expired = false;
        void HandleExpire(object? sender, Message e)
        {
            switch (e.Action)
            {
                case CoolDown.Action.Expire:
                    expired = true;
                    break;

            }
            Assert.Equal("ID", e.Id);
        }

        var duration = new TimeSpan(0, 0, 0, 1, 0);
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration, "ID");
        coolDown.Register(HandleExpire);
        coolDown.Start();
        Thread.Sleep(duration * 2);
        Assert.True(expired);
    }

    [Fact]
    public void TestCancellation()
    {
        DateTime? expiredAt = null;
        void HandleExpire(object? sender, Message e)
        {
            switch (e.Action)
            {
                case CoolDown.Action.Expire:
                    expiredAt = DateTime.Now;
                    break;
            }
            Assert.Equal("ID", e.Id);
        }

        var duration = new TimeSpan(0, 0, 0, 5, 0);
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration, "ID");
        coolDown.Register(HandleExpire);

        var started = coolDown.Start();
        Assert.True(started);

        Assert.Null(expiredAt);
        var cleared = coolDown.Clear();
        Assert.True(cleared);
        Assert.NotNull(expiredAt);
    }

    [Fact]
    public void TestRefresh()
    {
        DateTime? expiredAt = null;
        void HandleExpire(object? sender, Message e)
        {
            switch (e.Action)
            {
                case CoolDown.Action.Expire:
                    expiredAt = DateTime.Now;
                    break;
            }
            Assert.Equal("ID", e.Id);
        }

        var duration = new TimeSpan(0, 0, 0, 1, 0);
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration, "ID");
        coolDown.Register(HandleExpire);

        var started = coolDown.Start();
        Assert.True(started);

        Assert.Null(expiredAt);
        var cleared = coolDown.Refresh();
        Assert.True(cleared);
        Assert.Null(expiredAt);
        Thread.Sleep(duration * 2);
        Assert.NotNull(expiredAt);
    }
}
