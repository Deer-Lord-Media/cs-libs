using System.Collections.Generic;

namespace Pokemon.Data
{
    public struct Memory
    {
        public static Dictionary<int, Model> Pokemon = new Dictionary<int, Model>();

        public static Dictionary<int, Ability> Abilities = new Dictionary<int, Ability>();
    }
}
