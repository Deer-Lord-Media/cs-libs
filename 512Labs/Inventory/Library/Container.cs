namespace Inventory;

public class Container<T> where T : Data, new()
{
    public Container(int maximum, HashSet<Slot<T>>? slots = null)
    {
        this.maximum = maximum;
        this.members = new HashSet<Slot<T>>();
        int filledSlots = 0;
        if (slots != null)
        {
            foreach (Slot<T>? slot in slots)
            {
                this.members.Add(slot);
                filledSlots++;
            }
        }
    }

    private int maximum;
    public int Maximum { get { return this.maximum; } }
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

    public Slot<T>? Slot(int slotNumber)
    {
        Slot<T>? retval;
        var slots = this.Slots.Where(m => m.Number == slotNumber);
        retval = (slots.Count() > 0) ? slots.First() : null;
        return retval;
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
                Slot<T> slotC = new Slot<T>(slotA.Data, slotNumberB, slotA.Stacks);
                this.members.Add(slotC);
                this.members.Remove(slotA);
            }
            if (slotB != null)
            {
                Slot<T> slotD = new Slot<T>(slotB.Data, slotNumberA, slotB.Stacks);
                this.members.Add(slotD);
                this.members.Remove(slotB);
            }
            retval = true;
        }
        return retval;
    }
}
