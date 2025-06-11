using CoolDown;

namespace Buffs
{
    public class Buff
    {
        public Buff(Data data, CoolDown.Tracker coolDown)
        {
            this.data = data;
            this.coolDown = coolDown;
            this.stacks = 1;
        }
        public readonly Data data;
        public readonly CoolDown.Tracker coolDown;
        private int stacks;
        public int Stacks { get { return this.stacks; } }
        public bool Increment()
        {
            if (this.stacks < this.data.stacks)
            {
                this.stacks++;
                return true;
            }
            return false;
        }

        public bool Decrement()
        {
            if (this.stacks > 0)
            {
                this.stacks--;
                return true;
            }
            return false;
        }
    }
}
