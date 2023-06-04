namespace Buffs;

public abstract record Data(
    int Id,
    TimeSpan Duration,
    // buffs that block application
    HashSet<int>? SimilarTo = null,
    // buffs that this takes precedence over
    HashSet<int>? Overrides = null,
    int PrecedenceLevel = 0,
    int MaxStacks = 1
) : IEquatable<Data>
{
    internal HashSet<int> SimilarTo { get; init; } = (SimilarTo == null)
        ? new HashSet<int>() : new HashSet<int>(SimilarTo);
    internal HashSet<int> Overrides { get; init; } = (Overrides == null)
        ? new HashSet<int>() : new HashSet<int>(Overrides);

    public override int GetHashCode()
    {
        return this.Id;
    }
}
