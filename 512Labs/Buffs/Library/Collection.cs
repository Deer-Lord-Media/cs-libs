namespace Buffs;

/*
record YourData(int Id, TimeSpan Duration) : Buffs.Data(Id, Duration)
{

}

class Character
{
    private Buffs.Container<YourData> buffs;
    public HashSet<Buff<YourData>> Buffs { get { this.buffs.Active; } }
    
    public void ApplyBuff(YourData data)
    {
        this.buffs.Add(data);
    }
}

var character = new Character();
var data = new YourData(1, new TimeSpan(0, 0, 0, 1));
character.ApplyBuff(data);
*/

public class Collection<T> where T : Data
{
    public Collection(int maximum, IEnumerable<Buff<T>>? members = null)
    {
        this.Maximum = maximum;
        this.members = (members == null) ? new HashSet<Buff<T>>() : new HashSet<Buff<T>>(members);
    }

    private HashSet<Buff<T>> members;
    public int Maximum { get; init; }

    public bool IsFull { get { return this.members.Count() == this.Maximum; } }

    public HashSet<Buff<T>> Active
    {
        get
        {
            IEnumerable<Buff<T>> members = this.members.Where(m => !m.IsExpired && m.Stacks > 0);
            return new HashSet<Buff<T>>(members);
        }
    }

    // remove any expired buffs
    public bool Expire()
    {
        int count = this.members.RemoveWhere(m => m.IsExpired);
        return (count > 0);
    }

    // cancel an existing buff
    public bool Cancel(T data)
    {
        int count = this.members.RemoveWhere(m => m.Data == data);
        return (count > 0);
    }

    // apply a debuff `data` that removes buffs contained in `data.Overrides`
    public bool Debuff(T data)
    {
        int count = this.members.RemoveWhere(m => data.HasPrecedenceOver.Contains(m.Data.Id));
        return (count > 0);
    }

    public Status Apply(T data)
    {
        Status retval = Status.NotApplied;
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data == data);
        if (members.Count() > 0)
        // check for this buff
        {
            Buff<T> member = members.First();
            // complicated logic
            // if buff is expired, new application; new buff with 1 stack
            // else if not maxStacks, set stacks to +1
            // else just refresh with maxStacks
            int stacks = (member.IsExpired)
                ? 1
                : Math.Min(data.MaxStacks, member.Stacks + 1);
            // create new buff; refresh timer with appropriate stack count
            Buff<T> buff = new Buff<T>(data, stacks);
            this.members.Remove(member);
            this.members.Add(buff);
            retval = Status.Refreshed;
        } else
        {
            bool similar = this.similar(data);
            bool blocked = this.blocked(data);
            if (blocked)
            {
                retval = Status.Blocked;
            } else if (similar)
            {
                retval = Status.Similar;
            } else
            {
                // remove buffs that we override; mandatory
                this.members.RemoveWhere(m => data.HasPrecedenceOver.Contains(m.Data.Id));
                if (this.IsFull)
                // check for any buffs we can override and push the first one off
                {
                    members = this.members.Where(m => m.Data.PriorityLevel < data.PriorityLevel);
                    if (members.Count() > 0) this.members.Remove(members.First());
                }
                if (!this.IsFull)
                // if we have room now, add it
                {
                    Buff<T> buff = new Buff<T>(data);
                    this.members.Add(buff);
                    retval = Status.NewApplication;
                }
            }
        }
        return retval;
    }

    public bool Remove(T data)
    {
        bool removed = false;
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data == data);
        if (members.Count() > 0)
        {
            Buff<T> member = members.First();
            this.members.Remove(member);
            if (member.Stacks > 1)
            {
                member = new Buff<T>(member.Data, member.Stacks - 1);
                this.members.Add(member);
            }
            removed = true;
        }
        return removed;
    }

    public bool Contains(T data)
    {
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data == data);
        return (members.Count() > 0);
    }

    private bool similar(T data)
    {
        bool retval = false;
        if (data.IsSimilarTo.Count() > 0)
        {
            IEnumerable<Buff<T>> members = this.members
                .Where(m => data.IsSimilarTo.Contains(m.Data.Id) || m.Data.IsSimilarTo.Contains(data.Id));
            retval = (members.Count() > 0);
        }
        return retval;
    }

    private bool blocked(T data)
    {
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data.HasPrecedenceOver.Contains(data.Id));
        return (members.Count() > 0);
    }
}
