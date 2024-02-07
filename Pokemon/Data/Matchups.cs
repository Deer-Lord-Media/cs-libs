using System.Collections.Generic;

namespace Pokemon.Data
{
    public struct Matchups
    {
        public static double Factor(Type user, Type target)
        {
            return Table[user][target];
        }

        private static readonly Dictionary<Data.Type, double> Normal = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 1},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 1},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 1},
            {Data.Type.Rock, 0.5},
            {Data.Type.Ghost, 0},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Fire = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 0.5},
            {Data.Type.Water, 0.5},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 2},
            {Data.Type.Ice, 2},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 1},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 2},
            {Data.Type.Rock, 0.5},
            {Data.Type.Ghost, 0},
            {Data.Type.Dragon, 0.5}
        };

        private static readonly Dictionary<Data.Type, double> Water = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 2},
            {Data.Type.Water, 0.5},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 0.5},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 2},
            {Data.Type.Flying, 1},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 1},
            {Data.Type.Rock, 2},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 0.5}
        };

        private static readonly Dictionary<Data.Type, double> Electric = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 2},
            {Data.Type.Electric, 0.5},
            {Data.Type.Grass, 0.5},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 0},
            {Data.Type.Flying, 2},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 1},
            {Data.Type.Rock, 1},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 0.5}
        };

        private static readonly Dictionary<Data.Type, double> Grass = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 0.5},
            {Data.Type.Water, 2},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 0.5},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 0.5},
            {Data.Type.Ground, 2},
            {Data.Type.Flying, 0.5},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 0.5},
            {Data.Type.Rock, 2},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 0.5}
        };

        private static readonly Dictionary<Data.Type, double> Ice = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 0.5},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 2},
            {Data.Type.Ice, 0.5},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 2},
            {Data.Type.Flying, 2},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 1},
            {Data.Type.Rock, 1},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 2}
        };

        private static readonly Dictionary<Data.Type, double> Fighting = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 2},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 1},
            {Data.Type.Ice, 2},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 0.5},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 0.5},
            {Data.Type.Psychic, 0.5},
            {Data.Type.Bug, 0.5},
            {Data.Type.Rock, 2},
            {Data.Type.Ghost, 0},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Poison = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 2},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 0.5},
            {Data.Type.Ground, 0.5},
            {Data.Type.Flying, 1},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 2},
            {Data.Type.Rock, 0.5},
            {Data.Type.Ghost, 0.5},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Ground = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 2},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 2},
            {Data.Type.Grass, 0.5},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 2},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 0},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 0.5},
            {Data.Type.Rock, 2},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Flying = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 0.5},
            {Data.Type.Grass, 2},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 2},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 1},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 2},
            {Data.Type.Rock, 0.5},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Psychic = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 1},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 2},
            {Data.Type.Poison, 2},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 1},
            {Data.Type.Psychic, 0.5},
            {Data.Type.Bug, 1},
            {Data.Type.Rock, 1},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Bug = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 0.5},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 2},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, .5},
            {Data.Type.Poison, 2},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 0.5},
            {Data.Type.Psychic, 2},
            {Data.Type.Bug, 1},
            {Data.Type.Rock, 1},
            {Data.Type.Ghost, 0.5},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Rock = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 2},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 1},
            {Data.Type.Ice, 2},
            {Data.Type.Fighting, 0.5},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 0.5},
            {Data.Type.Flying, 2},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 2},
            {Data.Type.Rock, 1},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Ghost = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 0},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 1},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 1},
            {Data.Type.Psychic, 0},
            {Data.Type.Bug, 1},
            {Data.Type.Rock, 1},
            {Data.Type.Ghost, 2},
            {Data.Type.Dragon, 1}
        };

        private static readonly Dictionary<Data.Type, double> Dragon = new Dictionary<Data.Type, double>{
            {Data.Type.Normal, 1},
            {Data.Type.Fire, 1},
            {Data.Type.Water, 1},
            {Data.Type.Electric, 1},
            {Data.Type.Grass, 1},
            {Data.Type.Ice, 1},
            {Data.Type.Fighting, 1},
            {Data.Type.Poison, 1},
            {Data.Type.Ground, 1},
            {Data.Type.Flying, 1},
            {Data.Type.Psychic, 1},
            {Data.Type.Bug, 1},
            {Data.Type.Rock, 1},
            {Data.Type.Ghost, 1},
            {Data.Type.Dragon, 2}
        };

        private static readonly Dictionary<Data.Type, Dictionary<Data.Type, double>> Table = new Dictionary<Data.Type, Dictionary<Data.Type, double>>{
            {Data.Type.Normal, Normal},
            {Data.Type.Fire, Fire},
            {Data.Type.Water, Water},
            {Data.Type.Electric, Electric},
            {Data.Type.Grass, Grass},
            {Data.Type.Ice, Ice},
            {Data.Type.Fighting, Fighting},
            {Data.Type.Poison, Poison},
            {Data.Type.Ground, Ground},
            {Data.Type.Flying, Flying},
            {Data.Type.Psychic, Psychic},
            {Data.Type.Bug, Bug},
            {Data.Type.Rock, Rock},
            {Data.Type.Ghost, Ghost},
            {Data.Type.Dragon, Dragon}
        };
    }
}
