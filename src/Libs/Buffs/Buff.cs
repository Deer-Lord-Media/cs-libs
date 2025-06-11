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
        }

        public readonly Data data;
        public readonly CoolDown.Tracker coolDown;
        public readonly Resource.Tracker stacks;

        public override int GetHashCode()
        {
            return this.data.GetHashCode();
        }
    }
}
