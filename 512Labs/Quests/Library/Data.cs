namespace Quests;

public abstract record Data(int Id, int TotalStages = 1) : IEquatable<Data>
{
    public override int GetHashCode()
    {
        return this.Id;
    }
}
