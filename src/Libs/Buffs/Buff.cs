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

        /// <summary>
        /// Create a new instance with an updated event queue.
        /// </summary>
        /// <param name="events">The events queue to use for the new Buff.</param>
        /// <returns>A new instance of the existing Buff.</returns>
        public Buff Factory(CoolDown.Events events)
        {
            var coolDown = new CoolDown.Tracker(this.data.duration, events);
            return new Buff(this.data, coolDown);
        }

        /// <summary>
        /// Create a new instance with an updated cooldown.
        /// </summary>
        /// <param name="coolDown">The cooldown instance to use for the new Buff.</param>
        /// <returns>A new instance of the existing Buff.</returns>
        public Buff Factory(CoolDown.Tracker coolDown)
        {
            return new Buff(this.data, coolDown);
        }
    }
}
