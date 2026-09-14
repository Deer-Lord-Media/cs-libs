using System;
using System.Collections.Generic;
using BFS = Buffs;
using Xunit;
using CD = CoolDown;

namespace Tests.Buffs;

public class BuffsTest
{
    [Fact]
    public void TestDataNull()
    {
        var d1 = new BFS.Data(1, "data_one", new TimeSpan(), null, null, null);
        var equal = d1.Equals(null);
        Assert.False(equal);

        equal = d1.Equals(1);
        Assert.False(equal);
    }

    [Fact]
    public void TestDataNotRelated()
    {
        var d1 = new BFS.Data(1, "data_one", new TimeSpan(), null, null, null);
        var d2 = new BFS.Data(2, "data_two", new TimeSpan(), null, null, null);

        var related = d1 == d2;
        var notRelated = d1 != d2;

        Assert.False(related);
        Assert.True(notRelated);

        related = d1 == null;
        Assert.False(related);

        related = d2 == null;
        Assert.False(related);
    }

    [Fact]
    public void TestDataPrecedenceEmpty()
    {
        var d1 = new BFS.Data(1, "data_one", new TimeSpan(), null, null, null);
        var d2 = new BFS.Data(2, "data_two", new TimeSpan(), null, null, null);

        var precedence = d1 > d2;
        Assert.False(precedence);

        precedence = d1 < d2;
        Assert.False(precedence);

        precedence = d2 > d1;
        Assert.False(precedence);

        precedence = d2 < d1;
        Assert.False(precedence);
    }

    [Fact]
    public void TestDataPrecedenceNull()
    {
        var d1 = new BFS.Data(1, "data_one", new TimeSpan(), null, null, null);

        var precedence = d1 < null;
        Assert.False(precedence);

        precedence = d1 > null;
        Assert.True(precedence);

        precedence = null < d1;
        Assert.True(precedence);

        precedence = null > d1;
        Assert.False(precedence);
    }

    [Fact]
    public void TestDataPrecedence()
    {
        var d1 = new BFS.Data(1, "data_one", new TimeSpan(), null, null, null);
        var d2 = new BFS.Data(2, "data_two", new TimeSpan(), null, new HashSet<BFS.Data> { d1 }, null);

        var precedence = d1 < d2;
        Assert.True(precedence);

        precedence = d1 > d2;
        Assert.False(precedence);
    }

    [Fact]
    public void TestDataRelations()
    {
        var d1 = new BFS.Data(1, "data_one", new TimeSpan(), null, null, null);
        var d2 = new BFS.Data(2, "data_two", new TimeSpan(), null, null, new HashSet<BFS.Data> { d1 });
        var d3 = new BFS.Data(3, "data_three", new TimeSpan(), null, new HashSet<BFS.Data> { d1 }, null);
        var d4 = new BFS.Data(1, "data_four", new TimeSpan(), null, null, null);

        var notEqual = d1 == d2;
        Assert.True(notEqual);

        notEqual = d1 == d3;
        Assert.False(notEqual);

        notEqual = d1 != d2;
        Assert.False(notEqual);

        notEqual = d1 != d3;
        Assert.False(notEqual);

        notEqual = d2 != d3;
        Assert.True(notEqual);

        notEqual = d1 != null;
        Assert.True(notEqual);

        notEqual = null != d2;
        Assert.True(notEqual);

        var equal = d1 == d4;
        Assert.True(equal);
    }

    [Fact]
    public void TestBuffApply()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CD.Events();
        var tracker = new BFS.Tracker(1);
        var coolDown = new CD.Tracker(duration, events, false);

        var dataOne = new BFS.Data(0, "TestBuff", duration, null, null, null);
        var buffOne = new BFS.Buff(dataOne, coolDown);
        var actionOne = tracker.Apply(buffOne);
        Assert.Equal(BFS.Action.Applied, actionOne);

        var dataTwo = new BFS.Data(1, "TestBuff", duration, null, null, null);
        var buffTwo = new BFS.Buff(dataTwo, coolDown);
        var actionTwo = tracker.Apply(buffTwo);
        Assert.Equal(BFS.Action.Full, actionTwo);

        actionOne = tracker.Apply(buffOne);
        Assert.Equal(BFS.Action.Refreshed, actionOne);

        buffOne = tracker.Get(dataOne);
        Assert.Equal(1, buffOne.stacks.Value);

        var removeAction = tracker.Remove(dataOne);
        Assert.Equal(BFS.Action.Removed, removeAction);

        var emptied = tracker.Empty();
        Assert.True(emptied);
    }

    [Fact]
    public void TestBuffStack()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CD.Events();
        var tracker = new BFS.Tracker(1);
        var coolDown = new CD.Tracker(duration, events, false);

        var data = new BFS.Data(0, "TestBuff", duration, 2, null, null);
        var buffOne = new BFS.Buff(data, coolDown);
        var action = tracker.Apply(buffOne);
        Assert.Equal(BFS.Action.Applied, action);

        var buff = tracker.Get(data);
        Assert.Equal(1, buff.stacks.Value);

        action = tracker.Apply(buffOne);
        Assert.Equal(BFS.Action.Incremented, action);

        buff = tracker.Get(data);
        Assert.Equal(2, buff.stacks.Value);

        var decrementAction = tracker.Remove(data);
        Assert.Equal(BFS.Action.Decremented, decrementAction);

        var cleared = tracker.Clear(data);
        Assert.True(cleared);

        buff = tracker.Get(data);
        Assert.Null(buff);
    }

    [Fact]
    public void TestBuffPrecedence()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CD.Events();
        var tracker = new BFS.Tracker(1);
        var coolDown = new CD.Tracker(duration, events, false);

        var dataOne = new BFS.Data(0, "TestBuff", duration, null, null, null);
        var buffOne = new BFS.Buff(dataOne, coolDown);

        var dataTwo = new BFS.Data(1, "TestBuff", duration, null, [dataOne], null);
        var buffTwo = new BFS.Buff(dataTwo, coolDown);

        var actionOne = tracker.Apply(buffOne);
        Assert.Equal(BFS.Action.Applied, actionOne);

        var actionTwo = tracker.Apply(buffTwo);
        Assert.Equal(BFS.Action.Applied, actionTwo);

        actionOne = tracker.Apply(buffOne);
        Assert.Equal(BFS.Action.Blocked, actionOne);
    }

    [Fact]
    public void TestBuffExclusion()
    {
        var duration = new TimeSpan(0, 0, 0, 10, 0);
        var events = new CD.Events();
        var tracker = new BFS.Tracker(1);
        var coolDown = new CD.Tracker(duration, events, false);

        var dataOne = new BFS.Data(0, "TestBuff", duration, null, null, null);
        var buffOne = new BFS.Buff(dataOne, coolDown);

        var dataTwo = new BFS.Data(1, "TestBuff", duration, null, null, [dataOne]);
        var buffTwo = new BFS.Buff(dataTwo, coolDown);

        var actionOne = tracker.Apply(buffOne);
        Assert.Equal(BFS.Action.Applied, actionOne);

        var actionTwo = tracker.Apply(buffTwo);
        Assert.Equal(BFS.Action.Blocked, actionTwo);
    }
}
