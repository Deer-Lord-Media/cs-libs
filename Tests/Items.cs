using CSLibs.Items;

namespace Tests;

public class TestBag
{

    [Fact]
    public void CreateItemFromData()
    {
        int charges = 10;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 1000);
        var data = new CSLibs.Items.Data(
            1, "NAME", maxCharges: charges, coolDownDuration: duration
        );
        var item = new CSLibs.Items.Item(data);
        // initial creation from data
        Assert.Equal(charges, item.Charges);
        Assert.Equal(duration, item.CoolDown.Duration);

        // usability
        Assert.True(item.IsUsable);
    }

    [Fact]
    public void TestCoolDown()
    {
        var duration = new TimeSpan(0, 0, 1);
        var data = new CSLibs.Items.Data(
            1, "NAME", maxCharges: 10, coolDownDuration: duration
        );
        var item = new CSLibs.Items.Item(data);
        Assert.True(item.IsUsable);
        var used = item.Use();
        Assert.True(used);
        Assert.False(item.IsUsable);
        Thread.Sleep(duration);
        Assert.True(item.IsUsable);
    }

    [Fact]
    public void StoreAndStackItem()
    {
        var inventory = new Inventory(10);
        var duration = new TimeSpan(0, 0, 1);
        var data = new CSLibs.Items.Data(
            1, "NAME", maxCharges: 10, coolDownDuration: duration, maxStacks: 2
        );
        var item = new CSLibs.Items.Item(data);
        Assert.False(inventory.IsFull);
        bool stored = inventory.Store(item);
        Assert.True(stored);
        Slot? slot = inventory.Slot(1);
        Assert.NotNull(slot);
        Assert.Equal(1, slot.Number);
        Assert.Equal(1, slot.Stacks);
        foreach(int i in Enumerable.Range(0, 2))
        {
            stored = inventory.Store(item);
            Assert.True(stored);
        }
        slot = inventory.Slot(1);
        Assert.Equal(2, slot.Stacks);
        slot = inventory.Slot(2);
        Assert.Equal(1, slot.Stacks);
    }

    [Fact]
    public void FillUpInventory()
    {
        var inventory = new Inventory(2);
        var duration = new TimeSpan(0, 0, 1);
        var data = new CSLibs.Items.Data(
            1, "NAME", maxCharges: 10, coolDownDuration: duration, maxStacks: 1
        );
        var item = new CSLibs.Items.Item(data);
        bool stored;
        Assert.False(inventory.IsFull);
        foreach(int i in Enumerable.Range(0, 2))
        {
            stored = inventory.Store(item);
            Assert.True(stored);
        }
        stored = inventory.Store(item);
        Assert.False(stored);
    }

    [Fact]
    public void FillUpRange()
    {
        var inventory = new Inventory(20);
        var duration = new TimeSpan(0, 0, 1);
        var data = new CSLibs.Items.Data(
            1, "NAME", maxCharges: 10, coolDownDuration: duration, maxStacks: 1
        );
        var item = new CSLibs.Items.Item(data);
        bool stored;
        var range = new Tuple<int, int>(2, 3);
        Assert.False(inventory.IsFull);
        foreach(int i in Enumerable.Range(0, 2))
        {
            stored = inventory.Store(item, range: range);
            Assert.True(stored);
        }
        stored = inventory.Store(item, range: range);
        Assert.False(stored);
    }

    // [Fact]
    // public void StoreItem()
    // {
    //     // add an item
    //     var data = new CSLibs.Items.Data(1, "NAME");
    //     var bag = new CSLibs.Items.Inventory(20);
    //     Assert.Empty(bag.Slots);
    //     var success = bag.Store(data);
    //     Assert.True(success);
    //     Assert.Single(bag.Slots);
    //     var slot = bag.Slots.First();
    //     Assert.Equal(data, slot.Data);
    // }

    // [Fact]
    // public void StackAndRemoveSingle()
    // {
    //     // add 2 items with 2 max stacks, remove 
    //     var data = new CSLibs.Items.Data(1, false, 2);
    //     var bag = new CSLibs.Items.Bag(20);
    //     Assert.Empty(bag.Slots);
    //     foreach (int i in Enumerable.Range(1, 4))
    //     {
    //         var success = bag.Store(data);
    //         Assert.True(success);
    //     }
    //     var slot = bag.Slots.First();
    //     Assert.Equal(2, slot.Stacks);
    //     Assert.Equal(1, slot.Number);
    //     bag.Remove(data);
    //     slot = bag.Slots[0];
    //     Assert.Equal(1, slot.Stacks);
    //     Assert.Equal(1, slot.Number);
    //     slot = bag.Slots[1];
    //     Assert.Equal(2, slot.Stacks);
    //     Assert.Equal(2, slot.Number);
    // }

    // [Fact]
    // public void RemoveLastItem()
    // {
    //     var data = new CSLibs.Items.Data(1, false, 10);
    //     var bag = new CSLibs.Items.Bag(20);
    //     Assert.Empty(bag.Slots);
    //     var success = bag.Store(data);
    //     Assert.True(success);
    //     var slot = bag.Slots.First();
    //     Assert.Equal(1, slot.Stacks);
    //     success = bag.Remove(data);
    //     Assert.True(success);
    //     Assert.Empty(bag.Slots);
    // }

    // [Fact]
    // public void BagFull()
    // {
    //     var data = new CSLibs.Items.Data(1);
    //     var bag = new CSLibs.Items.Bag(2);
    //     Assert.Empty(bag.Slots);
    //     var success = bag.Store(data);
    //     Assert.True(success);
    //     success = bag.Store(data);
    //     Assert.True(success);
    //     Assert.Equal(2, bag.Slots.Count());
    //     var data2 = new CSLibs.Items.Data(2);
    //     success = bag.Store(data2);
    //     Assert.False(success);
    //     Assert.Equal(2, bag.Slots.Count());
    //     foreach (CSLibs.Items.Slot slot in bag.Slots)
    //     {
    //         Assert.Equal(1, slot.Data.Id);
    //     }
    // }

    // [Fact]
    // public void SwapSlotsTwoItems()
    // {
    //     var data1 = new CSLibs.Items.Data(1);
    //     var data2 = new CSLibs.Items.Data(2);
    //     var bag = new CSLibs.Items.Bag(2);
    //     var success = bag.Store(data1);
    //     Assert.True(success);
    //     success = bag.Store(data2);
    //     Assert.True(success);
    //     var first = bag.Slots[0];
    //     var second = bag.Slots[1];
    //     Assert.Equal(1, first.Data.Id);
    //     Assert.Equal(2, second.Data.Id);
    //     Assert.Equal(1, first.Number);
    //     Assert.Equal(2, second.Number);
    //     success = bag.SwapSlots(1, 2);
    //     Assert.True(success);
    //     first = bag.Slots[0];
    //     second = bag.Slots[1];
    //     Assert.Equal(2, first.Data.Id);
    //     Assert.Equal(1, second.Data.Id);
    //     Assert.Equal(1, first.Number);
    //     Assert.Equal(2, second.Number);
    // }

    // [Fact]
    // public void SwapSlotsItemToEmpty()
    // {
    //     var data = new CSLibs.Items.Data(1);
    //     var bag = new CSLibs.Items.Bag(2);
    //     var success = bag.Store(data);
    //     Assert.True(success);
    //     var slot = bag.Slots.First();
    //     Assert.Equal(1, slot.Number);
    //     success = bag.SwapSlots(1, 2);
    //     Assert.True(success);
    //     slot = bag.Slots.First();
    //     Assert.Equal(2, slot.Number);
    // }
}
