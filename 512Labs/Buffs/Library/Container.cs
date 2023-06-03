using System;
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
        public Container(int? maxbuffs = null)
    {
        this.MaxBuffs = maxbuffs;
        this.members = new HashSet<Buff<T>>();
    }

    private HashSet<Buff<T>> members;
    private int? MaxBuffs;

    public HashSet<Buff<T>> Active
    {
        get
        {
            HashSet<Buff<T>> buffs = new HashSet<Buff<T>>(this.members.Where(m => !m.Expired));
            return buffs;
        }
    }

    public bool Add(T data)
    {
        bool retval = false;
        if (this.MaxBuffs == null || this.members.Count() < this.MaxBuffs)
        {
            Buff<T> member;
            var found = this.members.Where(m => m.Data.Equals(data));
            if (found.Count() == 0)
            {
                member = new Buff<T>(data);
            } else
            {
                member = found.First();
                var s = member.Stacks;
                if (member.Stacks < member.Data.MaxStacks)
                {
                    this.members.Remove(member);
                    member = new Buff<T>(data);
                }
            }
            this.members.Add(member);
            retval = true;
        }
        return retval;
    }

    public bool? Remove(T data)
    {
        bool? retval = null;
        if(this.Has(data))
        {
            int count = this.members.RemoveWhere(m => m.Data.Equals(data));
            retval = count != 0;
        }
        return retval;
    }

    public int Expire()
    {
        return this.members.RemoveWhere(m => m.Expired);
    }

    public bool Has(T data)
    {
        var members = this.members.Where(m => m.Data.Equals(data));
        return (members.Count() != 0);
    }
}
