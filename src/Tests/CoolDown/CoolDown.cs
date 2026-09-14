using System;
using System.Threading;
using CD = CoolDown;
using Xunit;

namespace Tests.CoolDown;
#nullable enable

public class CoolDownTest
{
    [Fact]
    public void TestOperations()
    {
        var duration = new TimeSpan(0, 0, 0, 2, 0);
        var events = new CD.Events();
        var handledStart = false;
        var handledRefresh = false;
        var handledClear = false;
        var coolDown = new CD.Tracker(duration, events, false);

        void HandleStart(object? sender, CD.Message e)
        {
            Assert.Equal(CD.Action.Start, e.Action);
            handledStart = true;
        }
        events.Subscribe(HandleStart);
        var started = coolDown.Start();
        Assert.True(started);
        Assert.True(coolDown.IsActive);
        events.Unsubscribe(HandleStart);
        Assert.True(handledStart);
        Assert.InRange(coolDown.TimeRemaining, new TimeSpan(1), duration);

        void HandleRefresh(object? sender, CD.Message e)
        {
            switch (e.Action)
            {
                case CD.Action.Start:
                    handledRefresh = true;
                    break;
            }
        }
        events.Subscribe(HandleRefresh);

        var refreshed = coolDown.Start();
        Assert.True(refreshed);
        events.Unsubscribe(HandleRefresh);
        Assert.True(handledRefresh);

        void HandleClear(object? sender, CD.Message e)
        {
            Assert.Equal(CD.Action.Expire, e.Action);
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
        void Handle(object? sender, CD.Message e)
        {
            switch (e.Action)
            {
                case CD.Action.Expire:
                    expired = true;
                    break;
                case CD.Action.Start:
                    break;
            }
        }

        var duration = new TimeSpan(0, 0, 0, 1, 0);
        var events = new CD.Events();
        var coolDown = new CD.Tracker(duration, events, false);
        events.Subscribe(Handle);
        var started = coolDown.Start();
        Assert.True(started);
        Assert.True(coolDown.IsActive);
        Thread.Sleep(new TimeSpan(0, 0, 0, 3, 0));
        Assert.True(expired);
        events.Unsubscribe(Handle);
    }

    [Fact]
    public void TestCancellation()
    {
        DateTime? expiredAt = null;
        void HandleExpire(object? sender, CD.Message e)
        {
            switch (e.Action)
            {
                case CD.Action.Expire:
                    expiredAt = DateTime.Now;
                    break;
            }
        }

        var duration = new TimeSpan(0, 0, 0, 5, 0);
        var events = new CD.Events();
        var coolDown = new CD.Tracker(duration, events, false);
        events.Subscribe(HandleExpire);

        var started = coolDown.Start();
        Assert.True(started);
        Assert.True(coolDown.IsActive);

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
        void HandleExpire(object? sender, CD.Message e)
        {
            switch (e.Action)
            {
                case CD.Action.Expire:
                    expiredAt = DateTime.Now;
                    break;
            }
        }

        var duration = new TimeSpan(0, 0, 0, 2, 0);
        var events = new CD.Events();
        var coolDown = new CD.Tracker(duration, events, false);
        events.Subscribe(HandleExpire);

        var started = coolDown.Start();
        Assert.True(started);
        Assert.True(coolDown.IsActive);

        Assert.Null(expiredAt);
        var cleared = coolDown.Start();
        Assert.True(cleared);
        Assert.Null(expiredAt);
        Thread.Sleep(duration * 2);
        Assert.NotNull(expiredAt);

        events.Unsubscribe(HandleExpire);
    }
}
