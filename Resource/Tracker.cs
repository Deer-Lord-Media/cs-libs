using System;

namespace Resource;

public class Tracker
{
    public Tracker(string name, int maximum, int amount = 0)
    {
        this.Maximum = maximum;
        this.amount = amount;
        this.Name = name;
    }
    private int amount;
    public int Maximum { get; init; }
    public string Name { get; init; }
    public int Amount
    {
        get { return this.amount; }
        set { this.amount = Math.Min(this.Maximum, Math.Max(0, value)); }
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
        int remaining = Math.Max(this.Maximum - this.Amount, 0);
        if (remaining > 0)
        {
            toAdd = Math.Min(remaining, Math.Max(0, amount));
            this.Amount =+ toAdd;
        }
        return toAdd;
    }

    public bool IsFull
    {
        get
        {
            return this.Maximum == this.Amount;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return this.Amount == 0;
        }
    }
}
