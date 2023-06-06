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
    public Collection(int maximum, IEnumerable<Buff<T>>? buffs = null)
    {
        this.Maximum = maximum;
        this.members = (buffs == null) ? new HashSet<Buff<T>>() : new HashSet<Buff<T>>(buffs);
    }

    private HashSet<Buff<T>> members;
    public int Maximum { get; init; }

    public bool IsFull { get { return this.members.Count() >= this.Maximum; } }

    public IEnumerable<Buff<T>> Active
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
    public bool Cancel(T data)
    {
        int count = this.members.RemoveWhere(m => m.Data == data);
        return (count > 0);
    }

    // apply a debuff `data` that removes buffs contained in `data.Overrides`
    public bool Debuff(T data)
    {
        int count = this.members.RemoveWhere(m => data.HasPrecedenceOver(m.Data));
        return (count > 0);
    }

    public bool Apply(T data)
    {
        bool retval = false;
        IEnumerable<Buff<T>> buffs = this.members.Where(m => m.Data == data);
        if (buffs.Count() > 0)
        // check for this buff
        {
            Buff<T> buff = buffs.First();
            // complicated logic
            // if buff is expired, new application; new buff with 1 stack
            // else if not maxStacks, set stacks to +1
            // else just refresh with maxStacks
            int stacks = (buff.IsExpired)
                ? 1
                : Math.Min(data.MaxStacks, buff.Stacks + 1);
            // create new buff; refresh timer with appropriate stack count
            this.members.Remove(buff);
            buff = new Buff<T>(data, stacks);
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
                Buff<T> buff = new Buff<T>(data);
                this.members.Add(buff);
                retval = true;
            }
        }
        return retval;
    }

    public bool Remove(T data)
    {
        bool removed = false;
        IEnumerable<Buff<T>> buffs = this.members.Where(m => m.Data == data);
        if (buffs.Count() > 0)
        {
            Buff<T> buff = buffs.First();
            this.members.Remove(buff);
            if (buff.Stacks > 1)
            {
                buff = new Buff<T>(buff.Data, buff.Stacks - 1);
                this.members.Add(buff);
            }
            removed = true;
        }
        return removed;
    }

    public bool Contains(T data)
    {
        IEnumerable<Buff<T>> buffs = this.members.Where(m => m.Data == data);
        return (buffs.Count() > 0);
    }

    public bool Clear()
    {
        this.members.Clear();
        return this.members.Count() == 0;
    }

    private bool containsSimilar(T data)
    {
        bool retval = false;
        IEnumerable<Buff<T>> buffs = this.members
            .Where(m => data.IsSimilarTo(m.Data) || m.Data.IsSimilarTo(data));
        retval = (buffs.Count() > 0);
        return retval;
    }

    private bool containsBlocker(T data)
    {
        IEnumerable<Buff<T>> buffs = this.members.Where(m => m.Data.HasPrecedenceOver(data));
        return (buffs.Count() > 0);
    }
}
