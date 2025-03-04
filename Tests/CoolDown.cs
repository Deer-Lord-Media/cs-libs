using System;
using System.Threading;
using CoolDown;
using Xunit;

namespace Tests;
public class CoolDownTest
{
    [Fact]
    public void TestOperations()
    {
        var duration = new TimeSpan(0, 0, 0, 1, 0);
        var events = new Events();
        var handledStart = false;
        var handledRefresh = false;
        var handledClear = false;
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration, "ID", events);

        void HandleStart(object? sender, Message e)
        {
            Assert.Equal<CoolDown.Action>(CoolDown.Action.Start, e.Action);
            Assert.Equal("ID", e.Id);
            handledStart = true;
        }
        events.Subscribe(HandleStart);
        var started = coolDown.Start();
        Assert.True(started);
        events.Unsubscribe(HandleStart);
        Assert.True(handledStart);
        Assert.InRange<TimeSpan>(coolDown.TimeRemaining, new TimeSpan(1), duration);

        void HandleRefresh(object? sender, Message e)
        {
            Assert.Equal<CoolDown.Action>(CoolDown.Action.Start, e.Action);
            Assert.Equal("ID", e.Id);
            handledRefresh = true;
        }
        events.Subscribe(HandleRefresh);

        var refreshed = coolDown.Refresh();
        Assert.True(refreshed);
        events.Unsubscribe(HandleRefresh);
        Assert.True(handledRefresh);

        void HandleClear(object? sender, Message e)
        {
            Assert.Equal<CoolDown.Action>(CoolDown.Action.Expire, e.Action);
            Assert.Equal("ID", e.Id);
            handledClear = true;
        }
        events.Subscribe(HandleClear);
        var cleared = coolDown.Clear();
        Assert.True(cleared);
        events.Unsubscribe(HandleClear);
        Assert.True(handledClear);
    }

    [Fact]
    public void TestExpiry()
    {
        var expired = false;
        void Handle(object? sender, Message e)
        {
            switch (e.Action)
            {
                case CoolDown.Action.Expire:
                    expired = true;
                    break;
                case CoolDown.Action.Start:
                    break;
            }
        }

        var duration = new TimeSpan(0, 0, 0, 1, 0);
        var events = new Events();
        var coolDown = new CoolDown.Tracker(duration, "ID", events);
        events.Subscribe(Handle);
        var started = coolDown.Start();
        Assert.True(started);
        Thread.Sleep(new TimeSpan(0, 0, 0, 3, 0));
        Assert.True(expired);
        events.Unsubscribe(Handle);
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
        var events = new Events();
        var coolDown = new CoolDown.Tracker(duration, "ID", events);
        events.Subscribe(HandleExpire);

        var started = coolDown.Start();
        Assert.True(started);

        Assert.Null(expiredAt);
        var cleared = coolDown.Clear();
        Assert.True(cleared);
        Assert.NotNull(expiredAt);

        events.Unsubscribe(HandleExpire);
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
        var events = new Events();
        CoolDown.Tracker coolDown = new CoolDown.Tracker(duration, "ID", events);
        events.Subscribe(HandleExpire);

        var started = coolDown.Start();
        Assert.True(started);

        Assert.Null(expiredAt);
        var cleared = coolDown.Refresh();
        Assert.True(cleared);
        Assert.Null(expiredAt);
        Thread.Sleep(duration * 2);
        Assert.NotNull(expiredAt);

        events.Unsubscribe(HandleExpire);
    }
}
