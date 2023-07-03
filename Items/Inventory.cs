using System.Collections.Generic;
using System.Linq;
using System;

namespace CSLibs.Items
{
    public class Inventory
    {
        public Inventory(int maximum, IEnumerable<Slot>? slots = null)
        {
            this.Maximum = maximum;
            this.members = (slots == null) ? new HashSet<Slot>() : new HashSet<Slot>(slots);
        }

        private Dictionary<int, CoolDown> coolDowns = new Dictionary<int, CoolDown>();
        public int Maximum { get; }
        public bool IsFull { get { return this.members.Count() >= this.Maximum; } }
        private HashSet<Slot> members;

        private Slot? putInFreeSlot(Item item, int amount, int start, int end)
        {
            Slot? slot = null;

            if (item.Data.IsUnique)
            {
                IEnumerable<Slot> slots = this.members.Where(m => m.Item.Data == item.Data);
                if (slots.Count() > 0)
                {
                    return slot;
                }
            }

            HashSet<int> availableSlotNumbers = new HashSet<int>(
                // Range uses number of elements for the second argument, not
                //  the last integer in the range
                // end - start gets us that number, + 1 to prevent off by 1 bug
                Enumerable.Range(start, end - start + 1)
            );
            HashSet<int> usedSlotNumbers = new HashSet<int>(
                this.members
                    .Where(m => m.Number >= start && m.Number <= end)
                    .Select(m => m.Number)
            );
            availableSlotNumbers.ExceptWith(usedSlotNumbers);

            if (availableSlotNumbers.Count() > 0)
            {
                // get the first free slot number
                int number = availableSlotNumbers.OrderBy(i => i).First();

                // see if we have any items that share this cooldown
                // this should search the entire inventory, not just a subset
                var members = this.members
                    .Where(m => m.Item.Data.SharesCoolDown(item.Data));
                // get the cooldown if so, else make a new one
                CoolDown coolDown = (members.Count() > 0)
                    ? members.First().Item.CoolDown
                    : new CoolDown(item.Data.CoolDownDuration);
                // create a new item
                item = new Item(item.Data, item.Charges, coolDown: coolDown);
                // create a new slot
                slot = new Slot(item, number, amount);
                this.members.Add((Slot)slot);
            }
            return slot;
        }

        private bool addStacksToSlot(int slotNumber, int amount)
        {
            bool retval = false;
            Slot? slot = this.Slot(slotNumber);
            if (slot != null)
            {
                int stacks = slot.Stacks + amount;
                this.members.Remove(slot);
                Slot updated = new Slot(slot.Item, slot.Number, stacks);
                retval = this.members.Add(updated);
                if (!retval)
                {
                    this.members.Add(slot);
                }
            }
            return retval;
        }

        // returns how many are added
        public int Store(Item item, int amount = 1, Tuple<int, int>? range = null)
        {
            int retval = 0;
            int start;
            int end;

            if (range == null)
            {
                range = new Tuple<int, int>(1, this.Maximum);
            }
            start = Math.Max(1, range.Item1);
            end = Math.Min(this.Maximum, range.Item2);

            // check for unique
            var itemSlots = this.members.Where(m => m.Item.Data == item.Data);
            if (
                item.Data.IsUnique
                && itemSlots.Count() > 0
            )
            {
                return retval;
            }

            // check that we wont override 
            int totalOwned = itemSlots.Select(m => m.Stacks).Sum();
            if (item.Data.MaxOwned != 0 && item.Data.MaxOwned < totalOwned + amount)
            {
                amount = Math.Max(0, item.Data.MaxOwned - totalOwned);
            }

            var usedSlots = this.members.Where(
                m => m.Number >= start && m.Number <= end  
            );
            itemSlots = usedSlots.Where(m => m.Item.Data == item.Data);

            // find how many unfilled stacks can we fill up
            // we should also check for current total to see if we'll exceed MaxOwned
            int totalFree = itemSlots
                .Select(m => m.Item.Data.MaxStacks - m.Stacks)
                .Sum();

            // we need more space than we can stack on
            if (totalFree < amount)
            {
                int total = end+1 - start;
                int numFreeSlots = total - usedSlots.Count();
                int numFreeStacks = numFreeSlots * item.Data.MaxStacks;
                if (amount - totalFree > numFreeStacks)
                {
                    // we don't have space!!
                    return retval;
                }
            }

            // members = this.members.Where(
            //     m => m.Number >= start
            //         && m.Number <= end 
            //         && m.Item.Data == item.Data
            // );
            var slotData = new List<Tuple<int, int>>(
                this.members
                .Where(
                    m => m.Number >= start
                        && m.Number <= end
                        && m.Item.Data == item.Data
                )
                .Select(m => new Tuple<int, int>(m.Number, m.Stacks))
            );
            foreach (Tuple<int, int> data in slotData)
            {
                int number = data.Item1;
                int stacks = data.Item2;
                int freeSpace = item.Data.MaxStacks - stacks;
                int toAdd = Math.Max(0, Math.Min(freeSpace, amount));
                if (this.addStacksToSlot(number, toAdd))
                {
                    amount = Math.Max(0, amount - toAdd);
                    retval = retval + toAdd;
                }
            }
            while (amount > 0)
            {
                int stacks = Math.Max(0, Math.Min(amount, item.Data.MaxStacks));
                this.putInFreeSlot(item, stacks, start, end);
                amount = Math.Max(0, amount - stacks);
                retval = retval + stacks;
            }
            return retval;
        }

      public bool UseSlot(int number)
        {
            bool retval = false;

            IEnumerable<Slot> slots = this.members.Where(m => m.Number == number);
            if (slots.Count() > 0)
            {
                retval = slots.First().Item.Use();
            }
            return retval;
        }

        public bool Contains(Data data)
        {
            var members = this.members.Where(m => m.Item.Data == data && m.Stacks > 0);
            return members.Count() > 0;
        }

        public Slot? Slot(int slotNumber)
        {
            IEnumerable<Slot> members = this.members.Where(m => m.Number == slotNumber);
            return (members.Count() > 0) ? members.First() : null;
        }

        public bool SwapSlots(int slotNumberA, int slotNumberB)
        {
            bool retval = false;
            bool numberAinRange = (slotNumberA <= this.Maximum && 0 < slotNumberA);
            bool numberBinRange = (slotNumberB <= this.Maximum && 0 < slotNumberB);
            if (numberAinRange && numberBinRange)
            {
                Slot? slotA = this.Slot(slotNumberA);
                Slot? slotB = this.Slot(slotNumberB);
                if (slotA != null)
                {
                    this.members.Remove(slotA);
                    this.members.Add(new Slot(slotA.Item, slotNumberB, slotA.Stacks));
                    retval = true;
                }
                if (slotB != null)
                {
                    this.members.Remove(slotB);
                    this.members.Add(new Slot(slotB.Item, slotNumberA, slotB.Stacks));
                    retval = true;
                }
            }
            return retval;
        }
    }
}
