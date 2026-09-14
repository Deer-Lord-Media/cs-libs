using Xunit;

namespace Tests;

public class ResourceTest
{
    [Fact]
    public void TestResource()
    {
        var resource = new Resource.Tracker(100, 0);
        Assert.Equal(0, resource.Value);

        var success = resource.Increment();
        Assert.True(success);
        Assert.Equal(1, resource.Value);

        var added = resource.Add(200);
        Assert.Equal(99, added);
        Assert.Equal(100, resource.Value);

        success = resource.Increment();
        Assert.False(success);
        Assert.Equal(100, resource.Value);

        success = resource.Decrement();
        Assert.True(success);
        Assert.Equal(99, resource.Value);

        var subtracted = resource.Sub(200);
        Assert.Equal(99, subtracted);
        Assert.Equal(0, resource.Value);

        success = resource.Decrement();
        Assert.False(success);
        Assert.Equal(0, resource.Value);

        resource.Fill();
        Assert.Equal(100, resource.Value);

        resource.Empty();
        Assert.Equal(0, resource.Value);

        added = resource.Add(50);
        Assert.Equal(50, added);
        Assert.Equal(50, resource.Value);
        Assert.Equal(50, resource.Ratio);

        resource.Change(10, null);
        added = resource.Add(200);
        Assert.Equal(60, added);
        Assert.Equal(110, resource.Value);
    }
}
