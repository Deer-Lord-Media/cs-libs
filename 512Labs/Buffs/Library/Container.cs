namespace Buffs;

public class Container<T> where T : Data
{
    private HashSet<Tracker<T>> Members;
    private int? MaxBuffs;

    public Container(int? maxbuffs = null)
    {
        this.MaxBuffs = maxbuffs;
        this.Members = new HashSet<Tracker<T>>();
    }

    public HashSet<Buff<T>> Active
    {
        get
        {
            HashSet<Buff<T>> buffs = new HashSet<Buff<T>>();
            foreach (Tracker<T> member in this.Members.Where(m => m.Expired))
            {
                Buff<T> buff = new Buff<T>(member.Data, member.TimeLeft, member.Stacks);
                buffs.Add(buff);
            }
            return buffs;
        }
    }

    public bool Add(T data)
    {
        bool retval = false;
        if (this.MaxBuffs != null || this.MaxBuffs < this.Members.Count())
        {
            Tracker<T> member;
            Tracker<T>? current = this.Members.Where(m => m.Data.Equals(data)).FirstOrDefault();
            if (current == null)
            {
                member = new Tracker<T>(data);
            } else
            {
                member = (Tracker<T>)current;
                if (member.Stacks < member.Data.MaxStacks)
                {
                    this.Members.Remove(member);
                    member = new Tracker<T>(data);
                }
            }
            this.Members.Add(member);
            retval = true;
        }
        return retval;
    }

    public bool? Remove(T data)
    {
        bool? retval = null;
        if(this.Has(data))
        {
            int count = this.Members.RemoveWhere(m => m.Data.Equals(data));
            retval = count != 0;
        }
        return retval;
    }

    public void Expire()
    {
        this.Members.RemoveWhere(m => m.Expired);
    }

    public bool Has(T data)
    {
        Tracker<T>? tracker = this.Members.Where(m => m.Data.Equals(data)).FirstOrDefault();
        return (tracker != null);
    }
}
