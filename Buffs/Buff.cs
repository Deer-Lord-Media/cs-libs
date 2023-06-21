using System;

namespace CSLibs.Buffs
{
    public struct Buff
    {
        public Buff(Data data, int stacks = 1)
        {
            this.Data = data;
            this.Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
            this.At = DateTime.Now;
        }

        public Data Data { get; }
        public DateTime At { get; }
        public int Stacks { get; }

        public bool IsExpired
        {
            get
            {
                return this.TimeLeft <= new TimeSpan(0, 0, 0, 0, 0);
            } 
        }

        public TimeSpan TimeLeft
        {
            get
            {
                return this.Data.Duration - this.TimePassed;
            }
        }

        public TimeSpan TimePassed
        {
            get
            {
                return (DateTime.Now - this.At);
            }
        }
    }
}
