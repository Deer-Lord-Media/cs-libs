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
        get
        {
            return this.amount;
        }
        set
        {
            this.amount = Math.Min(this.Maximum, Math.Max(0, value));
        }
    }

    public bool Consume(int amount)
    {
        bool retval = false;
        if (amount < this.Amount)
        {
            this.Amount = this.amount - amount;
            retval = true;
        }
        return retval;
    }

    public int Replenish(int amount) // 200, 10
    {
        // 100 - 200 = 0
        // 100 - 10 = 90
        int remaining = Math.Max(this.Maximum - this.Amount, 0);
        int toAdd = Math.Min(remaining, amount);
        this.Amount =+ toAdd;
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
