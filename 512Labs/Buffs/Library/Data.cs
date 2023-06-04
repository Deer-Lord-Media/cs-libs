namespace Buffs;

/*
record YourData(int Id, TimeSpan Duration, int Other) : Buffs.Data(Id, Duration)
{

}
*/

public abstract record Data(int Id, TimeSpan Duration, int MaxStacks = 1) : IEquatable<Data>
{
    public override int GetHashCode()
    {
        return this.Id;
    }
}
