using Xunit;

namespace Tests;

public class ResourceTest
{
    [Fact]
    public void TestResource()
    {
        var resource = new Resource.Tracker(100);
        Assert.Equal<int>(0, resource.Value);

        var success = resource.Increment();
        Assert.True(success);
        Assert.Equal<int>(1, resource.Value);

        var added = resource.Add(200);
        Assert.Equal<int>(99, added);
        Assert.Equal<int>(100, resource.Value);

        success = resource.Increment();
        Assert.False(success);
        Assert.Equal<int>(100, resource.Value);

        success = resource.Decrement();
        Assert.True(success);
        Assert.Equal<int>(99, resource.Value);

        var subtracted = resource.Sub(200);
        Assert.Equal<int>(99, subtracted);
        Assert.Equal<int>(0, resource.Value);

        success = resource.Decrement();
        Assert.False(success);
        Assert.Equal<int>(0, resource.Value);

        resource.Fill();
        Assert.Equal<int>(100, resource.Value);

        resource.Empty();
        Assert.Equal<int>(0, resource.Value);

        added = resource.Add(50);
        Assert.Equal<int>(50, added);
        Assert.Equal<int>(50, resource.Value);
        Assert.Equal<float>(50, resource.Ratio);

        resource = resource.Change(10);
        added = resource.Add(200);
        Assert.Equal<int>(60, added);
        Assert.Equal<int>(110, resource.Value);
    }
}
