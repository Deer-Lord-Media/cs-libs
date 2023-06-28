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

        public bool Store(Item item)
        {
            Slot slot;
            bool added = false;

            var members = this.members.Where(m => m.Item.Data == item.Data);
            if (members.Count() > 0)
            {
                var member = members.First();

                if (!member.IsFull)
                {
                    this.members.Remove(member);
                    slot = new Slot(member.Item, member.Number, member.Stacks + 1);
                    this.members.Add(slot);
                    added = true;
                }
            }
            if (!added && !this.IsFull)
            {
                HashSet<int> availableSlotNumbers = new HashSet<int>(Enumerable.Range(1, this.Maximum));
                HashSet<int> usedSlotNumbers = new HashSet<int>(this.members.Select(m => m.Number));

                availableSlotNumbers.ExceptWith(usedSlotNumbers);

                if (availableSlotNumbers.Count() > 0)
                {
                    // get the first free slot number
                    int number = availableSlotNumbers.OrderBy(i => i).First();

                    // see if we have any items that share this cooldown
                    members = this.members
                        .Where(m => m.Item.Data.SharesCoolDown(item.Data));
                    // get the cooldown if so, else make a new one
                    CoolDown coolDown = (members.Count() > 0)
                        ? members.First().Item.CoolDown
                        : new CoolDown(item.Data.CoolDown);
                    // create a new item
                    item = new Item(item.Data, item.Charges, coolDown: coolDown);
                    // create a new slot
                    slot = new Slot(item, number, 1);
                    added = true;
                }
            }
            return added;
        }

        public bool UseSlot(int number)
        {
            bool retval = false;

            IEnumerable<Slot> slots = this.members.Where(m => m.Number == number);
            if (slots.Count() > 0)
            {
                Slot slot = slots.First();
                retval = slot.Item.Use();
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
