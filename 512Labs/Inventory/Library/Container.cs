namespace Inventory;

public class Container<T> where T : Data
{
    public Container(int maxSlots, List<Slot<T>>? slots = null)
    {
        this.maxSlots = maxSlots;
        if (slots == null)
        {
            this.members = new List<Slot<T>>();
        } else
        {
            this.members = new List<Slot<T>>(slots);
        }
    }

    private int maxSlots;
    public int MaxSlots { get { return this.maxSlots; } }
    private List<Slot<T>> members;
    public bool Full
    {
        get
        {
            return this.members.Count() >= this.MaxSlots;
        }
    }

    public bool Add(T data)
    {
        bool added = false;
        var slots = this.members.Where(m => m.Data.Equals(data));
        if (slots.Count() != 0)
        {
            foreach (Slot<T> slot in slots)
            {
                if (slot.Stacks < slot.Data.MaxStacks)
                {
                    // TODO: look at a better interface to add this
                    slot.Stacks++;
                    added = true;
                    break;
                }
            }
        }
        if (!added && !this.Full)
        {
            Slot<T> slot = new Slot<T>(data, 1);
            this.members.Add(slot);
            added = true;
        }
        return added;
    }

    public bool Remove(T data)
    {
        bool removed = false;
        var slots = this.members.Where(m => m.Data.Equals(data));
        if (slots.Count() != 0)
        {
            Slot<T> slot = slots.Last();
            slot.Stacks--;
            if (slot.Stacks == 0)
            {
                this.members.Remove(slot);
            }
            removed = true;
        }
        return removed;
    }
}
