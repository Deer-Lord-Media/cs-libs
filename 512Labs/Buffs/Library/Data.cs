namespace Buffs;

public abstract class Data : IEquatable<Data>
{
    public int Id;
    public TimeSpan Duration;
    public int MaxStacks= 1;

    public bool Equals(Data? obj)
    {
        return (obj != null) && this.Id == obj.Id;
    }
}
