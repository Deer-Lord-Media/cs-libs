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

        private bool putInFreeSlot(Item item, int amount, int start, int end)
        {
            bool added = false;

            HashSet<int> availableSlotNumbers = new HashSet<int>(
                    Enumerable.Range(start, end)
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
                var members = this.members
                    .Where(m => m.Item.Data.SharesCoolDown(item.Data));
                // get the cooldown if so, else make a new one
                CoolDown coolDown = (members.Count() > 0)
                    ? members.First().Item.CoolDown
                    : new CoolDown(item.Data.CoolDown);
                // create a new item
                item = new Item(item.Data, item.Charges, coolDown: coolDown);
                // create a new slot
                Slot slot = new Slot(item, number, amount);
                this.members.Add(slot);
                added = true;
            }
            return added;
        }

        public bool Store(Item item, int amount = 1, int startSlot = 1, int? endSlot = null)
        {
            IEnumerable<Slot> members;
            bool added = false;
            int _endSlot = (endSlot == null) ? this.Maximum : (int)endSlot;

            // don't add unique items we already have
            members = this.members.Where(m => m.Item.Data == item.Data && m.Item.Data.IsUnique);
            if (members.Count() > 0)
            {
                return added;
            }
            // find existing items in range that we can stack on
            members = this.members.Where(
                m => m.Item.Data == item.Data
                    && m.Number >= startSlot
                    && m.Number <= _endSlot
                );
            if (members.Count() > 0 && !members.First().IsFull)
            {
                Slot member = members.First();

                amount = member.Stacks + amount;
                int remainder = 0;
                if (amount > member.Item.Data.MaxStacks)
                {
                    remainder = amount - member.Item.Data.MaxStacks;
                    amount = member.Item.Data.MaxStacks;
                }
                // create a new item
                item = new Item(item.Data, coolDown: member.Item.CoolDown);
                added = this.putInFreeSlot(item, amount, startSlot, _endSlot);
                if (added && remainder > 0)
                {
                    added = this.putInFreeSlot(item, remainder, startSlot, _endSlot);
                }
            } else
            {
               added = this.putInFreeSlot(item, amount, startSlot, _endSlot);
            }
            return added;
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

        private Slot? Slot(int slotNumber)
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
