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
        public List<Slot> Slots { get { return new List<Slot>(this.members.OrderBy(m => m.Number)); } }
        private HashSet<Slot> members;
        public HashSet<Slot> Members { get { return this.members; } }

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

        public bool Store(Item item, int amount = 1, Tuple<int, int>? range = null)
        {
            int start;
            int end;
            Slot? slot;

            if (range == null)
            {
                range = new Tuple<int, int>(1, this.Maximum);
            }
            start = Math.Max(1, range.Item1);
            end = Math.Min(this.Maximum, range.Item2);
           
            // try to stack on existing items
            IEnumerable<Slot> members = this.members
                .Where(
                    m => m.Item.Data == item.Data 
                        && m.Stacks < item.Data.MaxStacks
                        && m.Number >= start
                        && m.Number <= end
                )
                .OrderBy(m => m.Number);
            if (members.Count() > 0)
            {
                Slot member = members.First();
                amount = member.Stacks + amount;
                // amount = 10 + 20;
                // amount = 30;
                int stacks = Math.Min(member.Item.Data.MaxStacks, amount);
                // stacks = Math.Min(20, 30);
                // stacks = 20;
                int remainder = Math.Max(0, amount - member.Item.Data.MaxStacks);
                // remainder = Math.Max(0, 30 - 20);
                // remainder = Math.Max(0, 10);
                // remainder = 10;

                // replace existing slot
                this.members.Remove(member);
                slot = this.putInFreeSlot(member.Item, stacks, start, end);

                if (
                    slot != null
                    && remainder > 0
                    && !this.Store(item, remainder, range)
                )
                {
                    this.members.Remove(slot);
                    this.members.Add(member);
                }
            } else
            {
                slot = this.putInFreeSlot(item, amount, start, end);
            }
            return (slot != null);
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
