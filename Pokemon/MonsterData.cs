using System;

namespace Pokemon
{
    public enum ExperienceType
    {
        Fast,
        MediumFast,
        MediumSlow,
        Slow,
        Erratic
    }

    public readonly struct MonsterData
    {
        public MonsterData(Type Type1, Type? Type2 = null, ExperienceType expType = ExperienceType.MediumSlow)
        {
            this.Type1 = Type1;
            this.Type2 = Type2;
            this.ExpType = expType;
        }
        public readonly Type Type1;
        public readonly Type? Type2;
        public readonly ExperienceType ExpType;

        public int ExperienceNeeded(int level)
        {
            int exp = this.ExpType switch
            {
                ExperienceType.Fast => this.experienceFast(level),
                ExperienceType.MediumFast => this.experienceMediumFast(level),
                ExperienceType.MediumSlow => this.experienceMediumSlow(level),
                ExperienceType.Slow => this.experienceSlow(level),
                ExperienceType.Erratic => this.experienceErratic(level),
                _ => throw new NotImplementedException()
            };
            return exp;
        }

        private int experienceFast(int level)
        {
            double exp = 4* Math.Pow(level, 3) / 5;
            return (int)Math.Ceiling(exp);
        }

        private int experienceMediumFast(int level)
        {
            double exp = Math.Pow(level, 3);
            return (int)Math.Ceiling(exp);
        }

        private int experienceMediumSlow(int level)
        {
            double exp = 6 / 5 * Math.Pow(level, 3);
            exp -= 15 * Math.Pow(level, 2);
            exp += 100 * level;
            exp -= 140;
            return (int)Math.Ceiling(exp);
        }

        private int experienceSlow(int level)
        {
            double exp = 5 * Math.Pow(level, 3) / 4;
            return (int)Math.Ceiling(exp);
        }

        private int experienceErratic(int level)
        {
            double exp = Math.Pow(level, 3);
            if (level < 50)
            {
                exp *= 100 - level;
                exp /= 50;
            } else if (level < 68)
            {
                exp *= 150 - level;
                exp /= 100;
            } else if (level < 98)
            {
                exp *= (1911 - 10 * level) / 3;
                exp /= 500;
            } else
            {
                exp *= 160 - level;
                exp /= 100;
            }
            return (int)Math.Ceiling(exp);
        }
    }
}