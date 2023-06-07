namespace Inventory;

public class Slot : IEquatable<Slot>
{
    public Slot(Data data, int slotNumber, int stacks = 1)
    {
        this.Data = data;
        this.Number = slotNumber;
        this.Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
    }
    public int Number { get; }
    public Data Data { get; }
    public int Stacks { get; }

    public bool IsFull
    {
        get
        {
            return this.Stacks >= this.Data.MaxStacks;
        }
    }

    public bool Equals(Slot? obj)
    {
        return (obj != null) && obj.Equals(this);
    }
}
