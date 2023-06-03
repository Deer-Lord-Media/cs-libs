namespace Inventory;

public class Slot<T> : IEquatable<Slot<T>> where T : Data, new() 
{
    public Slot(T data, int stacks = 1)
    {
        this.Data = data;
        this.Stacks = stacks;
    }
    public Data Data { get; }
    public int Stacks;

    public bool Equals(Slot<T>? obj)
    {
        return (obj != null) && obj.Equals(this);
    }
}
