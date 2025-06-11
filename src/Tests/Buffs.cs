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
        var coolDown = new CoolDown.Tracker(duration, events);

        var dataOne = new Buffs.Data(0, "TestBuff", duration, null, null, null);
        var buffOne = new Buffs.Buff(dataOne, coolDown);
        var actionOne = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Applied, actionOne);

        var dataTwo = new Buffs.Data(1, "TestBuff", duration, null, null, null);
        var buffTwo = new Buffs.Buff(dataTwo, coolDown);
        var actionTwo = tracker.Apply(buffTwo);
        Assert.Equal<Buffs.Action>(Buffs.Action.Full, actionTwo);

        actionOne = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Refreshed, actionOne);

        buffOne = tracker.Get(dataOne);
        Assert.Equal<int>(1, buffOne.stacks.Value);
    }

    [Fact]
    public void TestBuffStack()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CoolDown.Events();
        var tracker = new Buffs.Tracker(1);
        var coolDown = new CoolDown.Tracker(duration, events);

        var data = new Buffs.Data(0, "TestBuff", duration, 2, null, null);
        var buffOne = new Buffs.Buff(data, coolDown);
        var action = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Applied, action);

        var buff = tracker.Get(data);
        Assert.Equal<int>(1, buff.stacks.Value);

        action = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Incremented, action);

        buff = tracker.Get(data);
        Assert.Equal<int>(2, buff.stacks.Value);
    }

    [Fact]
    public void TestBuffPrecedence()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CoolDown.Events();
        var tracker = new Buffs.Tracker(1);
        var coolDown = new CoolDown.Tracker(duration, events);

        var dataOne = new Buffs.Data(0, "TestBuff", duration, null, null, null);
        var buffOne = new Buffs.Buff(dataOne, coolDown);

        var dataTwo = new Buffs.Data(1, "TestBuff", duration, null, [dataOne], null);
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
        var coolDown = new CoolDown.Tracker(duration, events);

        var dataOne = new Buffs.Data(0, "TestBuff", duration, null, null, null);
        var buffOne = new Buffs.Buff(dataOne, coolDown);

        var dataTwo = new Buffs.Data(1, "TestBuff", duration, null, null, [dataOne]);
        var buffTwo = new Buffs.Buff(dataTwo, coolDown);

        var actionOne = tracker.Apply(buffOne);
        Assert.Equal<Buffs.Action>(Buffs.Action.Applied, actionOne);

        var actionTwo = tracker.Apply(buffTwo);
        Assert.Equal<Buffs.Action>(Buffs.Action.Blocked, actionTwo);
    }
}
