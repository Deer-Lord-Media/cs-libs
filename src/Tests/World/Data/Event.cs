using System;
using System.Runtime.CompilerServices;
using World.Data;
using Xunit;

namespace Tests.World.Data;
#nullable enable

public class EventTest
{
    // Stage's shape (fields/constructor) isn't shown alongside Event, so we build
    // instances by allocating an uninitialized Stage and setting its `number`
    // field via reflection. This avoids depending on a specific Stage constructor.
    private static Stage CreateStage(int number)
    {
        object boxed = RuntimeHelpers.GetUninitializedObject(typeof(Stage));
        typeof(Stage).GetField("number")!.SetValue(boxed, number);
        return (Stage)boxed;
    }

    [Fact]
    public void TestConstructor_SetsIdAndCount()
    {
        var id = Guid.NewGuid();
        var stages = new[] { CreateStage(1), CreateStage(2), CreateStage(3) };

        var evt = new Event(id, stages);

        Assert.Equal(id, evt.id);
        Assert.Equal(3, evt.count);
    }

    [Fact]
    public void TestConstructor_OrdersStagesByNumber()
    {
        var stages = new[] { CreateStage(3), CreateStage(1), CreateStage(2) };

        var evt = new Event(Guid.NewGuid(), stages);

        Assert.Equal(1, evt.stages[0].number);
        Assert.Equal(2, evt.stages[1].number);
        Assert.Equal(3, evt.stages[2].number);
    }

    [Fact]
    public void TestConstructor_CountMatchesInputLength()
    {
        var stages = new[] { CreateStage(1), CreateStage(2) };

        var evt = new Event(Guid.NewGuid(), stages);

        Assert.Equal(stages.Length, evt.count);
    }

    [Fact]
    public void TestStage_ReturnsMatchingStage()
    {
        var stages = new[] { CreateStage(1), CreateStage(2), CreateStage(3) };
        var evt = new Event(Guid.NewGuid(), stages);

        var stage = evt.Stage(2);

        Assert.NotNull(stage);
        Assert.Equal(2, stage!.Value.number);
    }

    [Fact]
    public void TestStage_UnorderedInput_StillReturnsCorrectStage()
    {
        var stages = new[] { CreateStage(3), CreateStage(1), CreateStage(2) };
        var evt = new Event(Guid.NewGuid(), stages);

        Assert.Equal(1, evt.Stage(1)!.Value.number);
        Assert.Equal(2, evt.Stage(2)!.Value.number);
        Assert.Equal(3, evt.Stage(3)!.Value.number);
    }

    [Fact]
    public void TestStage_GapInNumbering_ReturnsNullForMismatchedPosition()
    {
        // Two stages, numbered 1 and 3: at index 1 (the "2nd stage" position)
        // sits stage number 3, which does not match, so Stage(2) should be null.
        var stages = new[] { CreateStage(1), CreateStage(3) };
        var evt = new Event(Guid.NewGuid(), stages);

        Assert.Null(evt.Stage(2));
        Assert.NotNull(evt.Stage(1));
    }

    [Fact]
    public void TestStage_NumberBelowRange_ClampsToFirstStage()
    {
        var stages = new[] { CreateStage(1), CreateStage(2) };
        var evt = new Event(Guid.NewGuid(), stages);

        var stage = evt.Stage(0);

        Assert.NotNull(stage);
        Assert.Equal(1, stage!.Value.number);
    }

    [Fact]
    public void TestStage_NumberAboveRange_ClampsToLastStage()
    {
        var stages = new[] { CreateStage(1), CreateStage(2) };
        var evt = new Event(Guid.NewGuid(), stages);

        var stage = evt.Stage(99);

        Assert.NotNull(stage);
        Assert.Equal(2, stage!.Value.number);
    }

    [Fact]
    public void TestStage_DoesNotThrowForOutOfRangeInput()
    {
        var stages = new[] { CreateStage(1), CreateStage(2) };
        var evt = new Event(Guid.NewGuid(), stages);

        var exception = Record.Exception(() => evt.Stage(-5));

        Assert.Null(exception);
    }
}