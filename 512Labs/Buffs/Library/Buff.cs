namespace Buffs;

public readonly struct Buff<T>
{
    public Buff(T data, TimeSpan timeLeft, int stacks)
    {
        this.Data = data;
        this.TimeLeft = timeLeft;
        this.Stacks = stacks;
    }
    public readonly T Data { get; }
    public readonly TimeSpan TimeLeft { get; }
    public readonly int Stacks { get; }
}
