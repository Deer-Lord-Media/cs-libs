using System;
using System.Reflection;
using Quest.Data;
using Xunit;

namespace Tests.Quest;
#nullable enable

public class ComparatorTest
{
    // Comparator is a readonly struct with no public constructor, so its fields
    // can only be set via reflection outside of Quest.Data itself. This helper
    // builds a Comparator with the given field values for testing purposes.
    private static Comparator Create(
        Symbol symbol,
        int value,
        Guid encounterId = default,
        Guid againstId = default,
        Against againstType = default)
    {
        object boxed = new Comparator();
        var type = typeof(Comparator);

        type.GetField(nameof(Comparator.encounterId))!.SetValue(boxed, encounterId);
        type.GetField(nameof(Comparator.againstId))!.SetValue(boxed, againstId);
        type.GetField(nameof(Comparator.againstType))!.SetValue(boxed, againstType);
        type.GetField(nameof(Comparator.symbol))!.SetValue(boxed, symbol);
        type.GetField(nameof(Comparator.value))!.SetValue(boxed, value);

        return (Comparator)boxed;
    }

    [Fact]
    public void TestFieldsAreSetCorrectly()
    {
        var encounterId = Guid.NewGuid();
        var againstId = Guid.NewGuid();
        var comparator = Create(Symbol.EQUAL, 10, encounterId, againstId);

        Assert.Equal(encounterId, comparator.encounterId);
        Assert.Equal(againstId, comparator.againstId);
        Assert.Equal(Symbol.EQUAL, comparator.symbol);
        Assert.Equal(10, comparator.value);
    }

    [Fact]
    public void TestCompare_GreaterThan()
    {
        var comparator = Create(Symbol.GREATER_THAN, 3);

        Assert.True(comparator.Compare(5));
        Assert.False(comparator.Compare(3));
        Assert.False(comparator.Compare(2));
    }

    [Fact]
    public void TestCompare_LessThan()
    {
        var comparator = Create(Symbol.LESS_THAN, 5);

        Assert.True(comparator.Compare(3));
        Assert.False(comparator.Compare(5));
        Assert.False(comparator.Compare(7));
    }

    [Fact]
    public void TestCompare_AtLeast()
    {
        var comparator = Create(Symbol.AT_LEAST, 3);

        Assert.True(comparator.Compare(3));
        Assert.True(comparator.Compare(5));
        Assert.False(comparator.Compare(2));
    }

    [Fact]
    public void TestCompare_OrLess()
    {
        var comparator = Create(Symbol.OR_LESS, 3);

        Assert.True(comparator.Compare(3));
        Assert.True(comparator.Compare(2));
        Assert.False(comparator.Compare(5));
    }

    [Fact]
    public void TestCompare_Equal()
    {
        var comparator = Create(Symbol.EQUAL, 4);

        Assert.True(comparator.Compare(4));
        Assert.False(comparator.Compare(5));
    }

    [Fact]
    public void TestCompare_AreSet()
    {
        var comparator = Create(Symbol.ARE_SET, 0b0110);

        Assert.True(comparator.Compare(0b1111));
        Assert.False(comparator.Compare(0b0100));
    }

    [Fact]
    public void TestCompare_NotSet()
    {
        var comparator = Create(Symbol.NOT_SET, 0b0110);

        Assert.True(comparator.Compare(0b1001));
        Assert.False(comparator.Compare(0b0110));
    }

    [Fact]
    public void TestCompare_AnyOf()
    {
        var comparator = Create(Symbol.ANY_OF, 0b0110);

        Assert.True(comparator.Compare(0b0010));
        Assert.False(comparator.Compare(0b1001));
    }

    [Fact]
    public void TestCompare_UnknownSymbol_ReturnsFalse()
    {
        var comparator = Create((Symbol)(-999), 0);

        Assert.False(comparator.Compare(0));
    }
}