using System;

namespace CSLibs.Buffs
{
    public struct Buff
    {
        public Buff(Data data, int stacks = 1, DateTime? at = null)
        {
            Data = data;
            Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
            At = (at != null) ? (DateTime)at : DateTime.Now;
            coolDown = new CoolDown.Tracker(data.Duration);
        }

        public readonly CoolDown.Tracker coolDown { get; }

        public readonly Data Data { get; }
        public readonly DateTime At { get; }
        public readonly int Stacks { get; }
        
        private readonly TimeSpan _zero {
            get
            {
                return new TimeSpan(0, 0, 0, 0, 0);
            }
        }

        public bool IsExpired
        {
            get
            {
                return (Data.Duration != _zero) && TimeLeft <= _zero;
            } 
        }

        public TimeSpan TimeLeft
        {
            get
            {
                return Data.Duration - TimePassed;
            }
        }

        public TimeSpan TimePassed
        {
            get
            {
                return DateTime.Now - At;
            }
        }
    }
}
