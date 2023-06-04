namespace Quests;

public abstract record Data : IEquatable<Data>
{

    public int Id { get; init; }
    public int TotalStages { get; init; }

    public override int GetHashCode()
    {
        return this.Id;
    }
}
