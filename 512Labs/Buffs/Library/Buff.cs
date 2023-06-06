namespace Buffs;

public struct Buff
{
    public Buff(Data data, int stacks = 1)
    {
        this.Data = data;
        this.Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
        this.At = DateTime.Now;
    }

    public Data Data { get; init; }
    public DateTime At { get; init; }
    public int Stacks { get; init; }

    public bool IsExpired
    {
        get
        {
            return this.TimeLeft.Milliseconds <= 0;
        } 
    }

    public TimeSpan TimeLeft
    {
        get
        {
            TimeSpan timeLeft = this.Data.Duration - (DateTime.Now - this.At);
            timeLeft = new TimeSpan(0, 0, 0, 0, Math.Max(0, timeLeft.Milliseconds));
            return timeLeft;
        }
    }
}
