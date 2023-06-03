namespace Inventory;

public class Container<T> where T : Data, new()
{
    public Container(int maximum, HashSet<Slot<T>>? slots = null)
    {
        this.Maximum = maximum;
        this.members = new HashSet<Slot<T>>();
        if (slots != null)
        {
            foreach (Slot<T>? slot in slots) this.members.Add(slot);
        }
    }

    public int Maximum { get; }
    private HashSet<Slot<T>> members;
    public bool Full
    {
        get
        {
            return this.members.Count() == this.Maximum;
        }
    }

    public List<Slot<T>> Slots
    {
        get
        {
            return new List<Slot<T>>(this.members.OrderBy(m => m.Number));
        }
    }

    public bool Add(T data)
    {
        bool added = false;
        IEnumerable<Slot<T>> members = this.members.Where(m => m.Data == data && m.Stacks < m.Data.MaxStacks);
        if (members.Count() > 0)
        {
            var member = members.First();
            Slot<T> slot = new Slot<T>(member.Data, member.Number, member.Stacks + 1);
            this.members.Remove(member);
            this.members.Add(slot);
            added = true;
        } else if (!this.Full)
        {
            HashSet<int> availableSlotNumbers = new HashSet<int>(Enumerable.Range(1, this.Maximum));
            HashSet<int> usedSlotNumbers = new HashSet<int>();
            foreach (Slot<T> member in this.members) usedSlotNumbers.Add(member.Number);
            availableSlotNumbers.ExceptWith(usedSlotNumbers);
            int number = availableSlotNumbers.First();
            this.members.Add(new Slot<T>(data, number));
            added = true;
        }
        return added;
    }

    public bool Remove(T data)
    {
        bool removed = false;
        IEnumerable<Slot<T>> members = this.members.Where(m => m.Data == data).OrderBy(m => m.Stacks);
        foreach (Slot<T> member in members)
        {
            this.members.Remove(member);
            if (member.Stacks > 1)
            {
                this.members.Add(new Slot<T>(member.Data, member.Number, member.Stacks - 1));
            }
            removed = true;
            break;
        }
        return removed;
    }

    public bool Contains(T data)
    {
        var members = this.members.Where(m => m.Data == data && m.Stacks > 0);
        return members.Count() > 0;
    }

    public Slot<T>? Slot(int slotNumber)
    {
        Slot<T>? slot;
        IEnumerable<Slot<T>> members = this.members.Where(m => m.Number == slotNumber);
        slot = (members.Count() > 0) ? members.First() : null;
        return slot;
    }

    public bool SwapSlots(int slotNumberA, int slotNumberB)
    {
        bool retval = false;
        bool numberAinRange = (slotNumberA <= this.Maximum && 0 < slotNumberA);
        bool numberBinRange = (slotNumberB <= this.Maximum && 0 < slotNumberB);
        if (numberAinRange && numberBinRange)
        {
            Slot<T>? slotA = this.Slot(slotNumberA);
            Slot<T>? slotB = this.Slot(slotNumberB);
            if (slotA != null)
            {
                this.members.Remove(slotA);
                Slot<T> slotC = new Slot<T>(slotA.Data, slotNumberB, slotA.Stacks);
                this.members.Add(slotC);
            }
            if (slotB != null)
            {
                this.members.Remove(slotB);
                Slot<T> slotD = new Slot<T>(slotB.Data, slotNumberA, slotB.Stacks);
                this.members.Add(slotD);
            }
            retval = true;
        }
        return retval;
    }
}
