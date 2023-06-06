namespace Buffs;

public record Data(
    int Id,
    // how long the buff lasts
    TimeSpan Duration,
    // buffs that block application
    HashSet<int>? SimilarTo = null,
    // buffs to remove, if they are present
    HashSet<int>? PrecedenceOver = null,
    // determines priority of buffs if collection is full
    int PriorityLevel = 0,
    int MaxStacks = 1
) : IEquatable<Data>
{
    private HashSet<int> similarTo { get; init; } = (SimilarTo == null)
        ? new HashSet<int>() : new HashSet<int>(SimilarTo);
    private HashSet<int> precedenceOver { get; init; } = (PrecedenceOver == null)
        ? new HashSet<int>() : new HashSet<int>(PrecedenceOver);

    public bool IsSimilarTo(Data data)
    {
        return (this.similarTo.Count() > 0) ? this.similarTo.Contains(data.Id) : false;
    }

    public bool HasPrecedenceOver(Data data)
    {
        return (this.precedenceOver.Count() > 0) ? this.precedenceOver.Contains(data.Id) : false;
    }

    public override int GetHashCode()
    {
        return this.Id;
    }
}
