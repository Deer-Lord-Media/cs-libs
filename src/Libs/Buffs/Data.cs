using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using CoolDown;

namespace Buffs
{
    public readonly struct Data
    {
        public readonly int Id;
        public readonly string Name;
        public readonly HashSet<Data> PrecedenceOver;
        public readonly HashSet<Data> EqualTo;
    }

}
