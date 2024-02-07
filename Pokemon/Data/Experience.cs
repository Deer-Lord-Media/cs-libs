using System;

namespace Pokemon.Data
{
    public readonly struct Experience
    {
        public Experience(ExperienceType expType)
        {
            this.expType = expType;
        }

        private readonly ExperienceType expType;
        public int Needed(int level)
        {
            int exp = this.expType switch
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