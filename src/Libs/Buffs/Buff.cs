using System.Reflection;
using CoolDown;
using Resource;

namespace Buffs
{
    public class Buff
    {
        public Buff(Data data, CoolDown.Tracker coolDown)
        {
            this.data = data;
            this.coolDown = coolDown;
            this.stacks = new Resource.Tracker(data.stacks);
            this.stacks.Increment();
        }

        public readonly Data data;
        public readonly CoolDown.Tracker coolDown;
        public readonly Resource.Tracker stacks;

        public Buff Factory(CoolDown.Events events)
        {
            var coolDown = new CoolDown.Tracker(this.data.duration, events);
            return new Buff(this.data, coolDown);
        }
        public Buff Factory(CoolDown.Tracker coolDown)
        {
            return new Buff(this.data, coolDown);
        }
    }
}
