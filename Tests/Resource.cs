using Resource;

namespace Tests;

public class ResourceTest
{
    [Fact]
    public void InitTracker()
    {
        var tracker = new Resource.Tracker("Health", 10);
        Assert.Equal(0, tracker.Amount);
    }

    [Fact]
    public void AddResource()
    {
        var tracker = new Resource.Tracker("Health", 10);
        tracker.Replenish(5);
        Assert.Equal(5, tracker.Amount);
    }

    [Fact]
    public void LimitMaximum()
    {
        var tracker = new Resource.Tracker("Health", 10);
        int added = tracker.Replenish(20);
        Assert.Equal(10, tracker.Amount);
        Assert.Equal(10, added);
    }

    [Fact]
    public void Consume()
    {
        var tracker = new Resource.Tracker("Health", 10, 5);
        Assert.Equal(5, tracker.Amount);
        bool consumed = tracker.Consume(3);
        Assert.True(consumed);
        Assert.Equal(2, tracker.Amount);
    }

    [Fact]
    public void UnableToConsume()
    {
        var tracker = new Resource.Tracker("Health", 10, 5);
        Assert.Equal(5, tracker.Amount);
        bool consumed = tracker.Consume(10);
        Assert.False(consumed);
        Assert.Equal(5, tracker.Amount);
    }


}
