namespace Inventory;

public abstract record Data(int Id, int MaxStacks = 1) : IEquatable<Data>
{
    public override int GetHashCode()
    {
        return this.Id;
    }
}