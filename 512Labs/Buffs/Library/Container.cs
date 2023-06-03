namespace Buffs;

/*
class YourDatazs : Buffs.Data
{
    // you can extend this class to have additional attributes and methods
}

class Character
{
    public Buffs.Container<YourData> Buffs;
}

var character = new Character();
var buff = new YourData();
character.Buffs.Add(buff);
*/
public class Container<T> where T : Data
{
        public Container(int maximum, IEnumerable<Buff<T>>? members = null)
    {
        this.Maximum = maximum;
        this.members = (members == null) ? new HashSet<Buff<T>>() : new HashSet<Buff<T>>(members);
    }

    private HashSet<Buff<T>> members;
    public int Maximum { get; }

    public bool Full
    {
        get
        {
            return this.members.Count() == this.Maximum;
        }
    }

    public HashSet<Buff<T>> Active
    {
        get
        {
            return new HashSet<Buff<T>>(this.members.Where(m => !m.IsExpired && m.Stacks > 0));
        }
    }

    public bool Add(T data)
    {
        bool added = false;
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data == data);
        if (members.Count() > 0)
        {
            Buff<T> member = members.First();
            if (member.Stacks < member.Data.MaxStacks)
            {
                Buff<T> buff = new Buff<T>(data, member.Stacks + 1);
                this.members.Remove(member);
                this.members.Add(buff);
                added = true;
            }
        } else if (!this.Full)
        {
            Buff<T> buff = new Buff<T>(data);
            this.members.Add(buff);
            added = true;
        }
        return added;
    }

    public bool Remove(T data)
    {
        bool removed = false;
        IEnumerable<Buff<T>> members = this.members.Where(m => m.Data == data);
        if (members.Count() > 0)
        {
            var member = members.First();
            this.members.Remove(member);
            if (member.Stacks > 0)
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
        var members = this.members.Where(m => m.Data == data);
        return (members.Count() > 0);
    }

    public int Expire()
    {
        return this.members.RemoveWhere(m => m.IsExpired);
    }
}
