using System;
using System.Collections.ObjectModel;

namespace CSLibs.Buffs
{
    public struct BuffCD
    {
        public BuffCD(Data data, int stacks = 1, CoolDown.Tracker? coolDown = null)
        {
            this.data = data;
            this.stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
            this.coolDown = (coolDown == null)
                ? new CoolDown.Tracker(data.Duration, "")
                : coolDown;
        }

        public readonly Data data { get; }
        public readonly int stacks { get; }
        public readonly CoolDown.Tracker? coolDown { get; }

        public readonly bool isExpired
        {
            get
            {
                if (this.coolDown == null)
                {
                    return false;
                }
                return this.coolDown.TimeRemaining <= new TimeSpan(0);
            }
        }
    }
    public struct Buff
    {
        public Buff(Data data, int stacks = 1, DateTime? at = null)
        {
            this.Data = data;
            this.Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
            this.At = (at != null) ? (DateTime)at : DateTime.Now;
            this.CoolDown = new CoolDown.Tracker(data.Duration, "");
        }

        public readonly Data Data { get; }
        public readonly DateTime At { get; }
        public readonly int Stacks { get; }

        public readonly CoolDown.Tracker? CoolDown;

        private readonly TimeSpan _zero
        {
            get
            {
                return new TimeSpan(0, 0, 0, 0, 0);
            }
        }

        public bool IsExpired
        {
            get
            {
                return (Data.Duration != _zero) && TimeRemaining <= _zero;
            }
        }

        public TimeSpan TimeRemaining
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
