using System;

namespace CSLibs.Resource
{
    public class Tracker
    {
        public Tracker(string name, int maximum, int amount = 0)
        {
            this.Maximum = maximum;
            this.Amount = amount;
            this.Name = name;
        }
        private int amount;
        public string Name { get; }
        public int Maximum { get; }
        public int Amount
        {
            get { return this.amount; }
            // must be between 0 and this.Maximum
            set { this.amount = Math.Min(this.Maximum, Math.Max(0, value)); }
        }

        public int Remaining
        {
            get { return this.Maximum - this.Amount; }
        }

        public bool Consume(int amount)
        {
            bool retval = false;
            if (amount < this.Amount)
            {
                this.Amount = this.Amount - amount;
                retval = true;
            }
            return retval;
        }

        public int Replenish(int amount)
        {
            int toAdd = 0;
            int remaining = this.Remaining;
            if (remaining > 0)
            {
                toAdd = Math.Min(remaining, Math.Max(0, amount));
                this.Amount = this.Amount + toAdd;
            }
            return toAdd;
        }

        public bool IsFull
        {
            get { return this.Maximum == this.Amount; }
        }

        public bool IsEmpty
        {
            get { return this.Amount == 0; }
        }
    }
}