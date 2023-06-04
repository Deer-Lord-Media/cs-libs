namespace Inventory;

public class Slot<T> : IEquatable<Slot<T>> where T : Data, new() 
{
    public Slot(T data, int slotNumber, int stacks = 1)
    {
        this.Data = data;
        this.Number = slotNumber;
        this.Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
    }
    public int Number { get; }
    public T Data { get; }
    public int Stacks { get; }

    public bool Full
    {
        get
        {
            return this.Stacks == this.Data.MaxStacks;
        }
    }

    public bool Equals(Slot<T>? obj)
    {
        return (obj != null) && obj.Equals(this);
    }
}
