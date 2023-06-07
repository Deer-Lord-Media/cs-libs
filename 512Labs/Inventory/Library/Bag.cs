namespace Inventory;

public class Bag
{
    public Bag(int maximum, IEnumerable<Slot>? slots = null)
    {
        this.Maximum = maximum;
        this.members = (slots == null) ? new HashSet<Slot>() : new HashSet<Slot>(slots);
    }

    public int Maximum { get; init; }
    private HashSet<Slot> members;
    public bool IsFull { get { return this.members.Count() >= this.Maximum; } }
    public List<Slot> Slots { get { return new List<Slot>(this.members.OrderBy(m => m.Number)); } }

    public bool Store(Data data)
    {
        bool retval = false;
        IEnumerable<Slot> members = this.members.Where(m => m.Data == data && !m.IsFull);
        if (members.Count() > 0)
        {
            Slot slot = members.First();
            if (!slot.Data.Unique)
            {   
                this.members.Remove(slot);
                this.members.Add(new Slot(slot.Data, slot.Number, slot.Stacks + 1));
                retval = true;
            }
        } else if (!this.IsFull)
        {
            HashSet<int> availableSlotNumbers = new HashSet<int>(Enumerable.Range(1, this.Maximum));
            HashSet<int> usedSlotNumbers = new HashSet<int>(this.members.Select(m => m.Data.Id));
            availableSlotNumbers.ExceptWith(usedSlotNumbers);
            int number = availableSlotNumbers.First();
            this.members.Add(new Slot(data, number));
            retval = true;
        }
        return retval;
    }

    public bool Remove(Data data)
    {
        bool removed = false;
        IEnumerable<Slot> members = this.members.Where(m => m.Data == data).OrderBy(m => m.Stacks);
        if (members.Count() > 0)
        {
            Slot slot = members.First();
            this.members.Remove(slot);
            if (slot.Stacks > 1)
            {
                this.members.Add(new Slot(slot.Data, slot.Number, slot.Stacks - 1));
            }
            removed = true;
        }
        return removed;
    }

    public bool Contains(Data data)
    {
        var members = this.members.Where(m => m.Data == data && m.Stacks > 0);
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
                this.members.Add(new Slot(slotA.Data, slotNumberB, slotA.Stacks));
                retval = true;
            }
            if (slotB != null)
            {
                this.members.Remove(slotB);
                this.members.Add(new Slot(slotB.Data, slotNumberA, slotB.Stacks));
                retval = true;
            }
        }
        return retval;
    }
}
