namespace Buffs;

/*
class YourData : Buffs.Data
{

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
            foreach (Tracker<T> member in this.Members.Where(m => !m.Expired))
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
        if (this.MaxBuffs == null || this.MaxBuffs > this.Members.Count())
        {
            Tracker<T> member;
            var found = this.Members.Where(m => m.Data.Equals(data));
            if (found.Count() == 0)
            {
                member = new Tracker<T>(data);
            } else
            {
                member = found.First();
                var s = member.Stacks;
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

    public int Expire()
    {
        return this.Members.RemoveWhere(m => m.Expired);
    }

    public bool Has(T data)
    {
        var trackers = this.Members.Where(m => m.Data.Equals(data));
        return (trackers.Count() != 0);
    }
}
