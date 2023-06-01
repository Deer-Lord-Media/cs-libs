namespace Buffs;

struct Tracker<T>  where T : Data
{
    public Tracker(T data, int stacks = 1)
    {
        this.Data = data;
        this.Stacks = stacks;
        this.At = DateTime.Now;
    }

    public T Data { get; }
    public DateTime At { get; }
    public int Stacks { get; }

    public bool Expired
    {
        get
        {
            return this.TimeLeft <= Tracker<T>.zeroTimeSpan;
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

    static TimeSpan zeroTimeSpan = new TimeSpan(0, 0, 0, 0, 0, 0);
}
