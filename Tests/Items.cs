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
        int amount = inventory.Store(item);
        Assert.Equal(1, amount);
        Slot? slot = inventory.Slot(1);
        Assert.NotNull(slot);
        Assert.Equal(1, slot.Number);
        Assert.Equal(1, slot.Stacks);
        foreach(int i in Enumerable.Range(0, 2))
        {
            amount = inventory.Store(item);
            Assert.Equal(1, amount);
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
        int added;
        Assert.False(inventory.IsFull);
        // seems to work fine the first time
        foreach(int i in Enumerable.Range(0, 1))
        {
            added = inventory.Store(item);
            Assert.Equal(1, added);
        }
        added = inventory.Store(item);
        Assert.Equal(1, added);
    }

    [Fact]
    public void FillUpRange()
    {
        int amount;
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
            amount = inventory.Store(item, range: range);
            //Assert.True(stored);
        }
        amount = inventory.Store(item, range: range);
       // Assert.False(stored);
    }

    [Fact]
    public void RespectSharedCoolDowns()
    {

    }

    [Fact]
    public void SwapSlotsTwoItems()
    {
        var data1 = new CSLibs.Items.Data(1, "NAME");
        var data2 = new CSLibs.Items.Data(2, "NAME");
        var bag = new CSLibs.Items.Inventory(2);
        var item1 = new Item(data1);
        var item2 = new Item(data2);
        int amount = bag.Store(item1);
        Assert.Equal(1, amount);
        amount = bag.Store(item2);
        Assert.Equal(1, amount);
        var first = bag.Slot(1);
        var second = bag.Slot(2);
        Assert.Equal(1, first.Item.Data.Id);
        Assert.Equal(2, second.Item.Data.Id);
        Assert.Equal(1, first.Number);
        Assert.Equal(2, second.Number);
        bool success = bag.SwapSlots(1, 2);
        Assert.True(success);
        first = bag.Slots[0];
        second = bag.Slots[1];
        Assert.Equal(2, first.Item.Data.Id);
        Assert.Equal(1, second.Item.Data.Id);
        Assert.Equal(1, first.Number);
        Assert.Equal(2, second.Number);
    }

    [Fact]
    public void SwapSlotsItemToEmpty()
    {
        var data = new CSLibs.Items.Data(1, "NAME");
        var item = new CSLibs.Items.Item(data);
        var bag = new CSLibs.Items.Inventory(2);
        int amount = bag.Store(item);
        Assert.Equal(1, amount);
        var slot = bag.Slots.First();
        Assert.Equal(1, slot.Number);
        bool success = bag.SwapSlots(1, 2);
        Assert.True(success);
        slot = bag.Slots.First();
        Assert.Equal(2, slot.Number);
    }
}
