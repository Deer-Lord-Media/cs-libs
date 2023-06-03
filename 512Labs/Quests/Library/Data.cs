namespace Quests;

public abstract class Data : IEquatable<Data>
{

    public int Id;
    public int TotalStages { get; }

    public bool Equals(Data? obj)
    {
        return (obj != null) && this.Id.Equals(obj.Id);
    }
}
