using World.Data;
using Xunit;

namespace Tests.World.Data;
#nullable enable

public class OperatorTest
{
    [Fact]
    public void TestFunction_KnownSymbols_ReturnsDelegate()
    {
        Assert.NotNull(Operator.Function(Symbol.GREATER_THAN));
        Assert.NotNull(Operator.Function(Symbol.LESS_THAN));
        Assert.NotNull(Operator.Function(Symbol.AT_LEAST));
        Assert.NotNull(Operator.Function(Symbol.OR_LESS));
        Assert.NotNull(Operator.Function(Symbol.EQUAL));
        Assert.NotNull(Operator.Function(Symbol.ARE_SET));
        Assert.NotNull(Operator.Function(Symbol.NOT_SET));
        Assert.NotNull(Operator.Function(Symbol.ANY_OF));
    }

    [Fact]
    public void TestFunction_UnknownSymbol_ReturnsNull()
    {
        var unknown = (Symbol)(-999);

        Assert.Null(Operator.Function(unknown));
    }

    [Fact]
    public void TestGreaterThan()
    {
        var func = Operator.Function(Symbol.GREATER_THAN)!;

        Assert.True(func(5, 3));
        Assert.False(func(3, 5));
        Assert.False(func(4, 4));
    }

    [Fact]
    public void TestLessThan()
    {
        var func = Operator.Function(Symbol.LESS_THAN)!;

        Assert.True(func(3, 5));
        Assert.False(func(5, 3));
        Assert.False(func(4, 4));
    }

    [Fact]
    public void TestAtLeast()
    {
        var func = Operator.Function(Symbol.AT_LEAST)!;

        Assert.True(func(5, 3));
        Assert.True(func(3, 3));
        Assert.False(func(2, 3));
    }

    [Fact]
    public void TestOrLess()
    {
        var func = Operator.Function(Symbol.OR_LESS)!;

        Assert.True(func(2, 3));
        Assert.True(func(3, 3));
        Assert.False(func(5, 3));
    }

    [Fact]
    public void TestEqual()
    {
        var func = Operator.Function(Symbol.EQUAL)!;

        Assert.True(func(4, 4));
        Assert.False(func(4, 5));
        Assert.True(func(-1, -1));
    }

    [Fact]
    public void TestAreSet()
    {
        var func = Operator.Function(Symbol.ARE_SET)!;

        Assert.True(func(0b1111, 0b0110));
        Assert.False(func(0b0100, 0b0110));
        Assert.True(func(0b0000, 0b0000));
        Assert.False(func(0b0000, 0b0001));
    }

    [Fact]
    public void TestNotSet()
    {
        var func = Operator.Function(Symbol.NOT_SET)!;

        Assert.True(func(0b1001, 0b0110));
        Assert.False(func(0b0110, 0b0110));
        Assert.False(func(0b0010, 0b0110));
        Assert.True(func(0b0000, 0b0000));
    }

    [Fact]
    public void TestAnyOf()
    {
        var func = Operator.Function(Symbol.ANY_OF)!;

        Assert.True(func(0b0010, 0b0110));
        Assert.False(func(0b1001, 0b0110));
        Assert.True(func(0b0110, 0b0110));
        Assert.False(func(0b0000, 0b0000));
    }
}
