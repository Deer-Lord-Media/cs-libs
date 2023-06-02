namespace Buffs;

// you subclass this with additional data/functionality you need
// this (probably) represents (part of) the data from your database
public abstract class Data : IEquatable<Data>
{
    // internal ID. you set this.
    public int Id;
    // how long this buff lasts
    public TimeSpan Duration;
    // how many instances of a buff that can be applied together
    // shared cooldown.
    public int MaxStacks = 1;

    public bool Equals(Data? obj)
    {
        return (obj != null) && this.Id.Equals(obj.Id);
    }
}
