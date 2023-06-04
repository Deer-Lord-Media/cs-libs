namespace Buffs;

public abstract record Data(int Id, TimeSpan Duration, int MaxStacks = 1) : IEquatable<Data>
{
    public override int GetHashCode()
    {
        return this.Id;
    }
}
