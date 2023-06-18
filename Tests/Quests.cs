using Quests;

namespace Tests;

public class TestLog
{
    [Fact]
    public void NoQuests()
    {
        var log = new Quests.Log(20);
        Assert.Empty(log.All);
    }

    [Fact]
    public void LogFullCantAccept()
    {
        var log = new Quests.Log(1);
        var data1 = new Quests.Data(1);
        var data2 = new Quests.Data(2);
        var success = log.Add(data1);
        Assert.True(success);
        success = log.Add(data2);
        Assert.False(success);
        Assert.Equal(1, log.All.Count());
    }

    [Fact]
    public void ConfirmContains()
    {
        var log = new Quests.Log(5);
        var data1 = new Quests.Data(1);
        var data2 = new Quests.Data(2);
        var success = log.Add(data1);
        Assert.True(success);
        var contains = log.Contains(data1);
        Assert.True(contains);
        contains = log.Contains(data2);
        Assert.False(contains);
    }

    [Fact]
    public void NoDuplicates()
    {
        var log = new Quests.Log(5);
        var data = new Quests.Data(1);
        var success = log.Add(data);
        Assert.True(success);
        success = log.Add(data);
        Assert.False(success);
        Assert.Equal(1, log.All.Count());
    }

    [Fact]
    public void AdvanceQuest()
    {
        var log = new Quests.Log(5);
        var data = new Quests.Data(1, 2);
        var success = log.Add(data);
        Assert.True(success);
        var quest = log.All.First();
        success = log.Advance(quest.Data);
        Assert.True(success);
    }

    [Fact]
    public void TrackQuest()
    {
        var log = new Quests.Log(5);
        var data = new Quests.Data(1, 2);
        log.Add(data);
        var quest = log.All.First();
        Assert.False(quest.Tracked);
        log.Track(data);
        quest = log.All.First();
        Assert.True(quest.Tracked);
        log.Untrack(quest.Data);
        quest = log.All.First();
        Assert.False(quest.Tracked);
    }
}
