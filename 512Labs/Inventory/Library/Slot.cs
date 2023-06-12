namespace Inventory;

public class Slot
{
    public Slot(Data data, int number, int stacks = 1)
    {
        this.Data = data;
        this.Number = number;
        this.Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
    }
    public int Number { get; init; }
    public Data Data { get; init; }
    public int Stacks { get; init; }

    public bool IsFull
    {
        get
        {
            return this.Stacks >= this.Data.MaxStacks;
        }
    }
}
