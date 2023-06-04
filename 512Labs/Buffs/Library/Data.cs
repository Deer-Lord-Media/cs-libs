namespace Buffs;

// you subclass this with additional data/functionality you need
// this (probably) represents (part of) the data from your database
public abstract record Data : IEquatable<Data>
{
    // internal ID. you set this.
    public int Id { get; init; }
    // how long this buff lasts
    public TimeSpan Duration { get; init; }
    // how many instances of a buff that can be applied together
    // shared cooldown.
    public int MaxStacks { get; init; } = 1;

    public override int GetHashCode()
    {
        return this.Id;
    }
}
