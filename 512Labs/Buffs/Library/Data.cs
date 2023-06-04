namespace Buffs;

public abstract record Data(
    int Id,
    TimeSpan Duration,
    // buffs that block application
    HashSet<int>? SimilarTo = null,
    // buffs that this takes precedence over
    HashSet<int>? PrecedenceOver = null,
    int MaxStacks = 1
) : IEquatable<Data>
{
    internal HashSet<int> SimilarTo { get; init; } = (SimilarTo == null) ? new HashSet<int>() : SimilarTo;
    internal HashSet<int> PrecedenceOver { get; init; } = (PrecedenceOver == null) ? new HashSet<int>() : PrecedenceOver;
    public override int GetHashCode()
    {
        return this.Id;
    }
}
