namespace Buffs;

/*
record YourData(int Id, TimeSpan Duration, int Other) : Buffs.Data(Id, Duration)
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
            IEnumerable<Buff<T>> members = this.members
                .Where(m => !m.IsExpired && m.Stacks > 0);
            return new HashSet<Buff<T>>(members);
        }
    }
    public int Expire()
    {
        return this.members.RemoveWhere(m => m.IsExpired);
    }

    public void Add(T data)
    {
        
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data == data);
        if (members.Count() > 0)
        // check for this buff, increment stacks if possible
        {
            Buff<T> member = members.First();
            if (member.Stacks < member.Data.MaxStacks)
            {
                Buff<T> buff = new Buff<T>(data, member.Stacks + 1);
                this.members.Remove(member);
                this.members.Add(buff);
            }
            return;
        } else if (!this.similar(data) && !this.blocked(data))
        // check for buffs that are similar, which we can't override
        // check for buffs that take precedence over us, we can't apply
        {
            // check for buffs we can override, remove them
            this.overrideWith(data);
            if (!this.Full)
            {
                Buff<T> buff = new Buff<T>(data);
                this.members.Add(buff);
            }
        }
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
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data.PrecedenceOver.Contains(data.Id));
        return (members.Count() > 0);
    }

    private bool overrideWith(T data)
    {
        int count = this.members.RemoveWhere(m => data.PrecedenceOver.Contains(m.Data.Id));
        return (count > 0);
    }
}
