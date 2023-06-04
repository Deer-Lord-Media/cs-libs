namespace Inventory;

public abstract record Data : IEquatable<Data>
{
    public int Id { get; init; }
    public int MaxStacks { get; init; }

    public override int GetHashCode()
    {
        return this.Id;
    }
}
