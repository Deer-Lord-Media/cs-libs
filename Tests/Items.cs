using CSLibs.Items;

namespace Tests;

public class TestBag
{

    [Fact]
    public void CreateItemFromData()
    {
        int charges = 10;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, 1000);
        var data = new Data(
            1, "NAME", maxCharges: charges, coolDownDuration: duration
        );
        var item = new Item(data);
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
        var data = new Data(
            1, "NAME", maxCharges: 10, coolDownDuration: duration
        );
        var item = new Item(data);
        Assert.True(item.IsUsable);
        var used = item.Use();
        Assert.True(used);
        Assert.False(item.IsUsable);
        Thread.Sleep(duration);
        Assert.True(item.IsUsable);
    }

    [Fact]
    public void ResetCoolDown()
    {
        var duration = new TimeSpan(0, 0, 1);
        var coolDown = new CoolDown(duration);
        Assert.True(coolDown.Ready);
        Assert.False(coolDown.Busy);
        var started = coolDown.Start();
        Assert.True(started);
        Assert.False(coolDown.Ready);
        Assert.True(coolDown.Busy);
        started = coolDown.Start();
        Assert.False(started);
        var reset = coolDown.Reset();
        Assert.True(reset);
        Assert.True(coolDown.Ready);
        Assert.False(coolDown.Busy);
    }
    [Fact]
    public void StoreAndStackItem()
    {
        var inventory = new Inventory(10);
        var duration = new TimeSpan(0, 0, 1);
        var data = new Data(
            1, "NAME", maxCharges: 10, coolDownDuration: duration, maxStacks: 2
        );
        var item = new Item(data);
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
        var data = new Data(
            1, "NAME", maxCharges: 10, coolDownDuration: duration, maxStacks: 1
        );
        var item = new Item(data);
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
        var data = new Data(
            1, "NAME", maxCharges: 10, coolDownDuration: duration, maxStacks: 1
        );
        var item = new Item(data);
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
        var data1 = new Data(1, "NAME", maxCharges: 5, coolDownDuration: new TimeSpan(0, 0, 5));
        var data2 = new Data(1, "NAME", maxCharges: 5);
        var coolDown = new CoolDown(new TimeSpan(0, 0, 1));
        var item1 = new Item(data1, coolDown: coolDown);
        var item2 = new Item(data2, coolDown: coolDown);
        var item3 = new Item(data1);
        Assert.True(item1.IsUsable);
        Assert.True(item2.IsUsable);
        bool used = item1.Use();
        Assert.True(used);
        Assert.False(item1.IsUsable);
        Assert.False(item2.IsUsable);
        used = item2.Use();
        Assert.False(used);
        Assert.True(item3.IsUsable);
    }

    [Fact]
    public void SwapSlotsTwoItems()
    {
        var data1 = new Data(1, "NAME");
        var data2 = new Data(2, "NAME");
        var bag = new Inventory(2);
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
        bool success = bag.SwapSlots(1, 2);
        Assert.True(success);
        first = bag.Slot(1);
        second = bag.Slot(2);
        Assert.Equal(2, first.Item.Data.Id);
        Assert.Equal(1, second.Item.Data.Id);
    }

    [Fact]
    public void SwapSlotsItemToEmpty()
    {
        var data = new Data(1, "NAME");
        var item = new Item(data);
        var bag = new Inventory(2);
        int amount = bag.Store(item);
        Assert.Equal(1, amount);
        var slot = bag.Slot(1);
        Assert.NotNull(slot);
        slot = bag.Slot(2);
        Assert.Null(slot);
        bool success = bag.SwapSlots(1, 2);
        Assert.True(success);
        slot = bag.Slot(1);
        Assert.Null(slot);
        slot = bag.Slot(2);
        Assert.NotNull(slot);
    }
}
