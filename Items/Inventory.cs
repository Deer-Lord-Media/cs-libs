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

        public int Maximum { get; }
        public bool IsFull { get { return this.members.Count() >= this.Maximum; } }
        public List<Slot> Slots { get { return new List<Slot>(this.members.OrderBy(m => m.Number)); } }
        private HashSet<Slot> members;

        public bool Store(Data data)
        {
            bool retval = false;

            IEnumerable<Slot> members = this.members.Where(m => m.Item.Data == data);
            int numberOwned = members.Select(m => m.Item.Data.Id).Sum();
            if (numberOwned < data.MaxOwned)
            {     
                members = members.Where(m => !m.IsFull);
                if (members.Count() > 0)
                {
                    Slot slot = members.First();
                    if (!slot.Item.Data.Unique)
                    {   
                        this.members.Remove(slot);
                        this.members.Add(new Slot(slot.Item, slot.Number, slot.Stacks + 1));
                        retval = true;
                    }
                } else if (!this.IsFull)
                {
                    HashSet<int> availableSlotNumbers = new HashSet<int>(Enumerable.Range(1, this.Maximum));
                    HashSet<int> usedSlotNumbers = new HashSet<int>(this.members.Select(m => m.Item.Data.Id));
                    availableSlotNumbers.ExceptWith(usedSlotNumbers);
                    Item item = new Item(data);
                    if (availableSlotNumbers.Count() > 0)
                    {
                        retval = this.members.Add(new Slot(item, availableSlotNumbers.First()));
                    }
                }
            }
            return retval;
        }

        public bool Remove(Item item)
        {
            bool removed = false;
            IEnumerable<Slot> members = this.members.Where(m => m.Item == item).OrderBy(m => m.Stacks);
            if (members.Count() > 0)
            {
                Slot slot = members.First();
                this.members.Remove(slot);
                if (slot.Stacks > 1)
                {
                    this.members.Add(new Slot(slot.Item, slot.Number, slot.Stacks - 1));
                }
                removed = true;
            }
            return removed;
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
        public bool CanUse(int slotNumber)
        {
            bool retval;

            Slot? slot = this.Slot(slotNumber);
            retval = (
                slot != null  // have slot
                && slot.Item.IsUsable  // isn't on cooldown itself
                && this.members
                    .Where(
                        m => slot.Item.Data.SharesCoolDown(m.Item.Data.Id)
                        && m.Item.OnCoolDown
                    ).Count() == 0  // nothing else on cooldown
            );
            return retval;
        }
    }
}
