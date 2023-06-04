namespace Buffs;

public abstract record Data(
    int Id,
    // how long the buff lasts
    TimeSpan Duration,
    // buffs that block application
    HashSet<int>? IsSimilarTo = null,
    // buffs to remove, if they are present
    HashSet<int>? HasPrecedenceOver = null,
    // determines priority of buffs if collection is full
    int PriorityLevel = 0,
    int MaxStacks = 1
) : IEquatable<Data>
{
    internal HashSet<int> IsSimilarTo { get; init; } = (IsSimilarTo == null)
        ? new HashSet<int>() : new HashSet<int>(IsSimilarTo);
    internal HashSet<int> HasPrecedenceOver { get; init; } = (HasPrecedenceOver == null)
        ? new HashSet<int>() : new HashSet<int>(HasPrecedenceOver);

    public override int GetHashCode()
    {
        return this.Id;
    }
}
