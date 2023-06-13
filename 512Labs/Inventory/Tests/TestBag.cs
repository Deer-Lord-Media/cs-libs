namespace Tests;

public class TestBag
{

    [Fact]
    public void StoreItem()
    {
        // add an item
        var data = new Inventory.Data(1);
        var bag = new Inventory.Bag(20);
        Assert.Empty(bag.Slots);
        bag.Store(data);
        Assert.Equal(1, bag.Slots.Count());
        var slot = bag.Slots.First();
        Assert.Equal(data, slot.Data);
    }

    [Fact]
    public void StackAndRemoveSingle()
    {
        // add 2 items with 2 max stacks, remove 
        var data = new Inventory.Data(1, false, 2);
        var bag = new Inventory.Bag(20);
        Assert.Empty(bag.Slots);
        bag.Store(data);
        bag.Store(data);
        bag.Store(data);
        bag.Store(data);
        var slot = bag.Slots.First();
        Assert.Equal(2, slot.Stacks);
        Assert.Equal(1, slot.Number);
        bag.Remove(data);
        slot = bag.Slots[0];
        Assert.Equal(1, slot.Stacks);
        Assert.Equal(1, slot.Number);
        slot = bag.Slots[1];
        Assert.Equal(2, slot.Stacks);
        Assert.Equal(2, slot.Number);
    }

    [Fact]
    public void RemoveLastItem()
    {
        var data = new Inventory.Data(1, false, 10);
        var bag = new Inventory.Bag(20);
        Assert.Empty(bag.Slots);
        bag.Store(data);
        var slot = bag.Slots.First();
        Assert.Equal(1, slot.Stacks);
        bag.Remove(data);
        Assert.Empty(bag.Slots);
    }

    [Fact]
    public void BagFull()
    {
        var data = new Inventory.Data(1);
        var bag = new Inventory.Bag(2);
        Assert.Empty(bag.Slots);
        bag.Store(data);
        bag.Store(data);
        Assert.Equal(2, bag.Slots.Count());
        var data2 = new Inventory.Data(2);
        bag.Store(data2);
        Assert.Equal(2, bag.Slots.Count());
        foreach (Inventory.Slot slot in bag.Slots)
        {
            Assert.Equal(1, slot.Data.Id);
        }
    }

    [Fact]
    public void SwapSlotsTwoItems()
    {
        var data1 = new Inventory.Data(1);
        var data2 = new Inventory.Data(2);
        var bag = new Inventory.Bag(2);
        bag.Store(data1);
        bag.Store(data2);
        var first = bag.Slots[0];
        var second = bag.Slots[1];
        Assert.Equal(1, first.Data.Id);
        Assert.Equal(2, second.Data.Id);
        Assert.Equal(1, first.Number);
        Assert.Equal(2, second.Number);
        bag.SwapSlots(1, 2);
        first = bag.Slots[0];
        second = bag.Slots[1];
        Assert.Equal(2, first.Data.Id);
        Assert.Equal(1, second.Data.Id);
        Assert.Equal(1, first.Number);
        Assert.Equal(2, second.Number);
    }

    [Fact]
    public void SwapSlotsItemToEmpty()
    {
        var data = new Inventory.Data(1);
        var bag = new Inventory.Bag(2);
        bag.Store(data);
        var slot = bag.Slots.First();
        Assert.Equal(1, slot.Number);
        bag.SwapSlots(1, 2);
        slot = bag.Slots.First();
        Assert.Equal(2, slot.Number);
    }
}
