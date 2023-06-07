namespace Inventory;

public record Data(int Id, bool Unique = false, int MaxStacks = 1) : IEquatable<Data>
{
    public override int GetHashCode()
    {
        return this.Id;
    }
}
