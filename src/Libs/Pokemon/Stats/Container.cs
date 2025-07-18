using System;
using System.Collections.Generic;

namespace Pokemon.Stats
{
    public class Container
    {
        public Resource.Tracker health;
        private readonly Stat hitPoints;
        private readonly Stat attack;
        private readonly Stat defense;
        private readonly Stat specialAttack;
        private readonly Stat specialDefense;
        private readonly Stat speed;
        private readonly Experience experience;
        public readonly Nature nature;
        private readonly Resource.Tracker evs;

        public int GetStat(Name name)
        {
            Func<int, Nature?, int> getValue;
            switch (name)
            {
                case Name.HitPoints:
                    getValue = hitPoints.Value;
                    break;
                case Name.Attack:
                    getValue = attack.Value;
                    break;
                case Name.Defense:
                    getValue = defense.Value;
                    break;
                case Name.SpecialAttack:
                    getValue = specialAttack.Value;
                    break;
                case Name.SpecialDefense:
                    getValue = specialDefense.Value;
                    break;
                case Name.Speed:
                    getValue = speed.Value;
                    break;
                default:
                    return 0;
            }
            return getValue(this.experience.level, this.nature);
        }

        public void GainEVs(int value, Name name)
        {
            value = this.evs.Add(value);
            if (value == 0) return;

            Resource.Tracker ev;

            switch (name)
            {
                case Name.HitPoints:
                    ev = hitPoints.ev;
                    break;
                case Name.Attack:
                    ev = attack.ev;
                    break;
                case Name.Defense:
                    ev = defense.ev;
                    break;
                case Name.SpecialAttack:
                    ev = specialAttack.ev;
                    break;
                case Name.SpecialDefense:
                    ev = specialAttack.ev;
                    break;
                case Name.Speed:
                    ev = speed.ev;
                    break;
                default:
                    return;
            }
            ev.Add(value);
        }

        public void LevelUp(int from)
        {
            if (from == this.experience.level) return;

            var maxHP = this.hitPoints.Value(this.experience.level, null);
            var change = maxHP - this.hitPoints.Value(from, this.nature);
            this.health.Change(maxHP, null);
            this.health.Add(change);
        }
    }

    public class Experience
    {
        public Experience(Resource.Tracker gained, List<(int min, int max)> table)
        {
            this.gained = gained;
            this.table = table;
        }

        private readonly Resource.Tracker gained;
        private List<(int min, int max)> table;

        public int total
        {
            get
            {
                return this.gained.Value;
            }
        }

        public int level
        {
            get
            {
                for (int i = 0; i < this.table.Count; i++)
                {
                    var (min, max) = this.table[i];
                    if (min <= this.gained.Value && max >= this.gained.Value) return i + 1;
                }
                return 1;
            }
        }

        public int Gain(int value)
        {
            return this.gained.Add(value);
        }
    }
}
