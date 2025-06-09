using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using CoolDown;

namespace Buffs
{
    public class Buff
    {
        public Buff(Data data, CoolDown.Tracker coolDown)
        {
            this.data = data;
            this.coolDown = coolDown;
        }
        public readonly Data data;
        public readonly CoolDown.Tracker coolDown;
    }
}
