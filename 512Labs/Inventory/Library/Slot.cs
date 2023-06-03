namespace Inventory;

public class Slot<T> where T : Data
{
    public Slot(T data, int stacks = 1)
    {
        this.Data = data;
        this.Stacks = stacks;
    }
    public Data Data { get; }
    public int Stacks;
}
