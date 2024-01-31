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
            set { this.amount = Math.Clamp(value, 0, this.Maximum); }
        }

        public int FreeSpace
        {
            get { return this.Maximum - this.Amount; }
        }

        public bool Consume(int amount)
        {
            bool consumed;
            
            if (amount < Amount)
            {
                amount = Math.Clamp(amount, 0, Amount);
                this.Amount -= amount;
                consumed = true;
            } else
            {
                consumed = false;
            }
            return consumed;
        }

        public int Replenish(int amount)
        {
            int replinished;

            replinished = Math.Clamp(amount, 0, FreeSpace);
            this.Amount += replinished;
            return replinished;
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