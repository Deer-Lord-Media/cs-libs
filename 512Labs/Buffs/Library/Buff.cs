namespace Buffs;

public struct Buff<T> where T : Data
{
    public Buff(T data, int stacks = 1)
    {
        this.Data = data;
        this.Stacks = stacks;
        this.At = DateTime.Now;
    }

    public T Data { get; }
    public DateTime At { get; }
    public int Stacks { get; }

    public bool IsExpired
    {
        get
        {
            return this.TimeLeft <= this.cachedZeroTimeSpan;
        }
    }

    public TimeSpan TimeLeft
    {
        get
        {
            TimeSpan timeLeft = this.Data.Duration - (DateTime.Now - this.At);
            timeLeft = new TimeSpan(0, 0, 0, 0, 0, Math.Max(0, timeLeft.Microseconds));
            return timeLeft;
        }
    }

    private TimeSpan cachedZeroTimeSpan = new TimeSpan(0, 0, 0, 0, 0, 0);
}
