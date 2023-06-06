namespace Buffs;

public class Tracker
{
    public Tracker(int maximum, IEnumerable<Buff>? buffs = null)
    {
        this.Maximum = maximum;
        this.members = (buffs == null) ? new HashSet<Buff>() : new HashSet<Buff>(buffs);
    }

    private HashSet<Buff> members;
    public int Maximum { get; init; }

    public bool IsFull { get { return this.members.Count() >= this.Maximum; } }

    public IEnumerable<Buff> Active
    {
        get
        {
            return this.members.Where(m => !m.IsExpired && m.Stacks > 0);
        }
    }

    // remove any expired buffs
    public bool Expire()
    {
        int count = this.members.RemoveWhere(m => m.IsExpired);
        return (count > 0);
    }

    // cancel an existing buff
    public bool Cancel(Data data)
    {
        int count = this.members.RemoveWhere(m => m.Data == data);
        return (count > 0);
    }

    // apply a debuff `data` that removes buffs contained in `data.Overrides`
    public bool Debuff(Data data)
    {
        int count = this.members.RemoveWhere(m => data.HasPrecedenceOver(m.Data));
        return (count > 0);
    }

    public bool Apply(Data data)
    {
        bool retval = false;
        IEnumerable<Buff> buffs = this.members.Where(m => m.Data == data);
        if (buffs.Count() > 0)
        // check for this buff
        {
            Buff buff = buffs.First();
            // complicated logic
            // if buff is expired, new application; new buff with 1 stack
            // else if not maxStacks, set stacks to +1
            // else just refresh with maxStacks
            int stacks = (buff.IsExpired)
                ? 1
                : Math.Min(data.MaxStacks, buff.Stacks + 1);
            // create new buff; refresh timer with appropriate stack count
            this.members.Remove(buff);
            buff = new Buff(data, stacks);
            this.members.Add(buff);
            retval = true;
        } else if (!this.containsBlocker(data) && !this.containsSimilar(data))
        {
            // remove buffs that we override; mandatory
            this.members.RemoveWhere(m => data.HasPrecedenceOver(m.Data));
            if (this.IsFull)
            // check for any buffs we can override and push the first one off
            {
                buffs = this.members.Where(m => m.Data.PriorityLevel < data.PriorityLevel);
                if (buffs.Count() > 0) this.members.Remove(buffs.First());
            }
            if (!this.IsFull)
            // if we have room, add it
            {
                Buff buff = new Buff(data);
                this.members.Add(buff);
                retval = true;
            }
        }
        return retval;
    }

    public bool Remove(Data data)
    {
        bool removed = false;
        IEnumerable<Buff> buffs = this.members.Where(m => m.Data == data);
        if (buffs.Count() > 0)
        {
            Buff buff = buffs.First();
            this.members.Remove(buff);
            if (buff.Stacks > 1)
            {
                buff = new Buff(buff.Data, buff.Stacks - 1);
                this.members.Add(buff);
            }
            removed = true;
        }
        return removed;
    }

    public bool Contains(Data data)
    {
        IEnumerable<Buff> buffs = this.members.Where(m => m.Data == data);
        return (buffs.Count() > 0);
    }

    public bool Clear()
    {
        this.members.Clear();
        return this.members.Count() == 0;
    }

    private bool containsSimilar(Data data)
    {
        bool retval = false;
        IEnumerable<Buff> buffs = this.members
            .Where(m => data.IsSimilarTo(m.Data) || m.Data.IsSimilarTo(data));
        retval = (buffs.Count() > 0);
        return retval;
    }

    private bool containsBlocker(Data data)
    {
        IEnumerable<Buff> buffs = this.members.Where(m => m.Data.HasPrecedenceOver(data));
        return (buffs.Count() > 0);
    }
}
