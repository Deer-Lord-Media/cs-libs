namespace Inventory;

public class Container<T> where T : Data, new()
{
    public Container(int maxSlots, List<Slot<T>?>? slots = null)
    {
        this.maxSlots = maxSlots;
        this.members = new List<Slot<T>?>();
        int usedSlots = 0;
        if (slots != null)
        {
            foreach (Slot<T>? slot in slots)
            {
                this.members.Add(slot);
                usedSlots++;
            }
        }

        for (int i = 0; i < (maxSlots - usedSlots); i++)
        {
            this.members.Add(null);
        }
    }

    private int maxSlots;
    public int MaxSlots { get { return this.maxSlots; } }
    private List<Slot<T>?> members;
    public bool Full
    {
        get
        {
            return this.members.Where(m => m == null).Count() == 0;
        }
    }

    public List<Slot<T>?> All { get { return new List<Slot<T>?>(this.members); } }

    public bool Add(T data)
    {
        bool added = false;
        var slots = this.members.Where(m => m != null && m.Data.Equals(data));
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
            int index = this.members.FindIndex(m => m == null);
            this.members[index] = new Slot<T>(data, 1);
            added = true;
        }
        return added;
    }

    public bool Remove(T data)
    {
        bool removed = false;
        var slots = this.members.Where(m => m != null && m.Data.Equals(data));
        if (slots.Count() != 0)
        {
            Slot<T> slot = slots.Last();
            slot.Stacks -= 1;
            if (slot.Stacks == 0)
            {
                int index = this.members.FindIndex(m => m == slot);
                this.members[index] = null;
            }
            removed = true;
        }
        return removed;
    }
}
