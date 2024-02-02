using System.Collections.Generic;

namespace Pokemon
{
    public class Matchups
    {
        public static Dictionary<Pokemon.Type, double> Normal = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 1},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 1},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 1},
            {Pokemon.Type.Rock, 0.5},
            {Pokemon.Type.Ghost, 0},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Fire = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 0.5},
            {Pokemon.Type.Water, 0.5},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 2},
            {Pokemon.Type.Ice, 2},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 1},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 2},
            {Pokemon.Type.Rock, 0.5},
            {Pokemon.Type.Ghost, 0},
            {Pokemon.Type.Dragon, 0.5}
        };

        public static Dictionary<Pokemon.Type, double> Water = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 2},
            {Pokemon.Type.Water, 0.5},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 0.5},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 2},
            {Pokemon.Type.Flying, 1},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 1},
            {Pokemon.Type.Rock, 2},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 0.5}
        };

        public static Dictionary<Pokemon.Type, double> Electric = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 2},
            {Pokemon.Type.Electric, 0.5},
            {Pokemon.Type.Grass, 0.5},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 0},
            {Pokemon.Type.Flying, 2},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 1},
            {Pokemon.Type.Rock, 1},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 0.5}
        };

        public static Dictionary<Pokemon.Type, double> Grass = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 0.5},
            {Pokemon.Type.Water, 2},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 0.5},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 0.5},
            {Pokemon.Type.Ground, 2},
            {Pokemon.Type.Flying, 0.5},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 0.5},
            {Pokemon.Type.Rock, 2},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 0.5}
        };

        public static Dictionary<Pokemon.Type, double> Ice = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 0.5},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 2},
            {Pokemon.Type.Ice, 0.5},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 2},
            {Pokemon.Type.Flying, 2},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 1},
            {Pokemon.Type.Rock, 1},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 2}
        };

        public static Dictionary<Pokemon.Type, double> Fighting = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 2},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 1},
            {Pokemon.Type.Ice, 2},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 0.5},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 0.5},
            {Pokemon.Type.Psychic, 0.5},
            {Pokemon.Type.Bug, 0.5},
            {Pokemon.Type.Rock, 2},
            {Pokemon.Type.Ghost, 0},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Poison = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 2},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 0.5},
            {Pokemon.Type.Ground, 0.5},
            {Pokemon.Type.Flying, 1},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 2},
            {Pokemon.Type.Rock, 0.5},
            {Pokemon.Type.Ghost, 0.5},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Ground = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 2},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 2},
            {Pokemon.Type.Grass, 0.5},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 2},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 0},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 0.5},
            {Pokemon.Type.Rock, 2},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Flying = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 0.5},
            {Pokemon.Type.Grass, 2},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 2},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 1},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 2},
            {Pokemon.Type.Rock, 0.5},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Psychic = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 1},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 2},
            {Pokemon.Type.Poison, 2},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 1},
            {Pokemon.Type.Psychic, 0.5},
            {Pokemon.Type.Bug, 1},
            {Pokemon.Type.Rock, 1},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Bug = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 0.5},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 2},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, .5},
            {Pokemon.Type.Poison, 2},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 0.5},
            {Pokemon.Type.Psychic, 2},
            {Pokemon.Type.Bug, 1},
            {Pokemon.Type.Rock, 1},
            {Pokemon.Type.Ghost, 0.5},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Rock = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 2},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 1},
            {Pokemon.Type.Ice, 2},
            {Pokemon.Type.Fighting, 0.5},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 0.5},
            {Pokemon.Type.Flying, 2},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 2},
            {Pokemon.Type.Rock, 1},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Ghost = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 0},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 1},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 1},
            {Pokemon.Type.Psychic, 0},
            {Pokemon.Type.Bug, 1},
            {Pokemon.Type.Rock, 1},
            {Pokemon.Type.Ghost, 2},
            {Pokemon.Type.Dragon, 1}
        };

        public static Dictionary<Pokemon.Type, double> Dragon = new Dictionary<Pokemon.Type, double>{
            {Pokemon.Type.Normal, 1},
            {Pokemon.Type.Fire, 1},
            {Pokemon.Type.Water, 1},
            {Pokemon.Type.Electric, 1},
            {Pokemon.Type.Grass, 1},
            {Pokemon.Type.Ice, 1},
            {Pokemon.Type.Fighting, 1},
            {Pokemon.Type.Poison, 1},
            {Pokemon.Type.Ground, 1},
            {Pokemon.Type.Flying, 1},
            {Pokemon.Type.Psychic, 1},
            {Pokemon.Type.Bug, 1},
            {Pokemon.Type.Rock, 1},
            {Pokemon.Type.Ghost, 1},
            {Pokemon.Type.Dragon, 2}
        };

        public static Dictionary<Pokemon.Type, Dictionary<Pokemon.Type, double>> Table = new Dictionary<Pokemon.Type, Dictionary<Pokemon.Type, double>>{
            {Pokemon.Type.Normal, Normal},
            {Pokemon.Type.Fire, Fire},
            {Pokemon.Type.Water, Water},
            {Pokemon.Type.Electric, Electric},
            {Pokemon.Type.Grass, Grass},
            {Pokemon.Type.Ice, Ice},
            {Pokemon.Type.Fighting, Fighting},
            {Pokemon.Type.Poison, Poison},
            {Pokemon.Type.Ground, Ground},
            {Pokemon.Type.Flying, Flying},
            {Pokemon.Type.Psychic, Psychic},
            {Pokemon.Type.Bug, Bug},
            {Pokemon.Type.Rock, Rock},
            {Pokemon.Type.Ghost, Ghost},
            {Pokemon.Type.Dragon, Dragon}
        };
    }
}
