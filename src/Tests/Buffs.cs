using System;
using System.Collections.Generic;
using Buffs;
using CoolDown;
using Xunit;

namespace Tests;

public class BuffsTest
{
    [Fact]
    public void TestBuffRefresh()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CoolDown.Events();
        var tracker = new Buffs.Tracker(1);
        var coolDown = new CoolDown.Tracker(duration, 1, events);

        var data = new Buffs.Data(0, "TestBuff", null, null, null);
        var buffOne = new Buffs.Buff(data, coolDown);
        var action = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Applied, action);

        data = new Buffs.Data(1, "TestBuff", null, null, null);
        var buffTwo = new Buffs.Buff(data, coolDown);
        action = tracker.Apply(buffTwo);
        Assert.Equal<Buffs.Action>(Buffs.Action.Full, action);

        action = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Refreshed, action);

        Assert.Equal<int>(1, buffOne.Stacks);
    }

    [Fact]
    public void TestBuffStack()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CoolDown.Events();
        var tracker = new Buffs.Tracker(1);
        var coolDown = new CoolDown.Tracker(duration, 1, events);

        var data = new Buffs.Data(0, "TestBuff", 2, null, null);
        var buffOne = new Buffs.Buff(data, coolDown);
        var action = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Applied, action);

        action = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Incremented, action);

        Assert.Equal<int>(2, buffOne.Stacks);
    }

    [Fact]
    public void TestBuffPrecedence()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CoolDown.Events();
        var tracker = new Buffs.Tracker(1);
        var coolDown = new CoolDown.Tracker(duration, 1, events);

        var dataOne = new Buffs.Data(0, "TestBuff", null, null, null);
        var buffOne = new Buffs.Buff(dataOne, coolDown);

        var dataTwo = new Buffs.Data(1, "TestBuff", null, [dataOne], null);
        var buffTwo = new Buffs.Buff(dataTwo, coolDown);

        var actionOne = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Applied, actionOne);

        var actionTwo = tracker.Apply(buffTwo);
        Assert.Equal<Buffs.Action>(Buffs.Action.Applied, actionTwo);

        actionOne = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Blocked, actionOne);
    }

    [Fact]
    public void TestBuffExclusion()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CoolDown.Events();
        var tracker = new Buffs.Tracker(1);
        var coolDown = new CoolDown.Tracker(duration, 1, events);

        var dataOne = new Buffs.Data(0, "TestBuff", null, null, null);
        var buffOne = new Buffs.Buff(dataOne, coolDown);

        var dataTwo = new Buffs.Data(1, "TestBuff", null, null, [dataOne]);
        var buffTwo = new Buffs.Buff(dataTwo, coolDown);

        var actionOne = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Applied, actionOne);

        var actionTwo = tracker.Apply(buffTwo);
        Assert.Equal<Buffs.Action>(Buffs.Action.Blocked, actionTwo);
    }
}
