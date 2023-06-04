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

    public bool Full { get { return this.members.Count() == this.Maximum; } }

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

    // apply a debuff `data` that removes buffs contained in `data.PrecedenceOver`
    public bool Debuff(T data)
    {
        int count = this.members.RemoveWhere(m => data.Overrides.Contains(m.Data.Id));
        return (count > 0);
    }

    public BuffStatus Apply(T data)
    {
        BuffStatus retval = BuffStatus.NotApplied;
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data == data);
        if (members.Count() > 0)
        // check for this buff
        {
            Buff<T> member = members.First();
            // increment stacks if possible
            int stacks = (member.Stacks < member.Data.MaxStacks) ? 1 : 0;
            Buff<T> buff = new Buff<T>(data, member.Stacks + stacks);
            this.members.Remove(member);
            this.members.Add(buff);
            retval = (stacks == 1) ? BuffStatus.StacksIncreased : BuffStatus.Refreshed;
        } else
        {
            bool similar = this.similar(data);
            bool blocked = this.blocked(data);
            if (blocked)
            {
                retval = BuffStatus.Blocked;
            } else if (similar)
            {
                retval = BuffStatus.Similar;
            } else
            {
                bool canAdd = false;
                // remove buffs that we override
                this.members.RemoveWhere(m => data.Overrides.Contains(m.Data.Id));
                if (this.Full)
                {
                    // push off buffs that are lower
                    members = this.members.Where(m => m.Data.PrecedenceLevel < data.PrecedenceLevel);
                    if (members.Count() > 0)
                    {
                        Buff<T> member = members.First();
                        this.members.Remove(member);
                        canAdd = true;
                    }
                } else
                {
                    canAdd = true;
                }
                if (canAdd)
                {
                    Buff<T> buff = new Buff<T>(data);
                    this.members.Add(buff);
                    retval = BuffStatus.NewApplication;
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
        if (data.SimilarTo.Count() > 0)
        {
            IEnumerable<Buff<T>> members = this.members
                .Where(m => data.SimilarTo.Contains(m.Data.Id) || m.Data.SimilarTo.Contains(data.Id));
            retval = (members.Count() > 0);
        }
        return retval;
    }

    private bool blocked(T data)
    {
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data.Overrides.Contains(data.Id));
        return (members.Count() > 0);
    }
}
