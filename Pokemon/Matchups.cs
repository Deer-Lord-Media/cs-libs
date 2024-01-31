using System.Collections.Generic;

namespace monsters
{
    public class Matchups
    {
        public static Dictionary<Element, double> Normal = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 1},
            {Element.Water, 1},
            {Element.Electric, 1},
            {Element.Grass, 1},
            {Element.Ice, 1},
            {Element.Fighting, 1},
            {Element.Poison, 1},
            {Element.Ground, 1},
            {Element.Flying, 1},
            {Element.Psychic, 1},
            {Element.Bug, 1},
            {Element.Rock, 0.5},
            {Element.Ghost, 0},
            {Element.Dragon, 1}
        };

        public static Dictionary<Element, double> Fire = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 0.5},
            {Element.Water, 0.5},
            {Element.Electric, 1},
            {Element.Grass, 2},
            {Element.Ice, 2},
            {Element.Fighting, 1},
            {Element.Poison, 1},
            {Element.Ground, 1},
            {Element.Flying, 1},
            {Element.Psychic, 1},
            {Element.Bug, 2},
            {Element.Rock, 0.5},
            {Element.Ghost, 0},
            {Element.Dragon, 0.5}
        };

        public static Dictionary<Element, double> Water = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 2},
            {Element.Water, 0.5},
            {Element.Electric, 1},
            {Element.Grass, 0.5},
            {Element.Ice, 1},
            {Element.Fighting, 1},
            {Element.Poison, 1},
            {Element.Ground, 2},
            {Element.Flying, 1},
            {Element.Psychic, 1},
            {Element.Bug, 1},
            {Element.Rock, 2},
            {Element.Ghost, 1},
            {Element.Dragon, 0.5}
        };

        public static Dictionary<Element, double> Electric = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 1},
            {Element.Water, 2},
            {Element.Electric, 0.5},
            {Element.Grass, 0.5},
            {Element.Ice, 1},
            {Element.Fighting, 1},
            {Element.Poison, 1},
            {Element.Ground, 0},
            {Element.Flying, 2},
            {Element.Psychic, 1},
            {Element.Bug, 1},
            {Element.Rock, 1},
            {Element.Ghost, 1},
            {Element.Dragon, 0.5}
        };

        public static Dictionary<Element, double> Grass = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 0.5},
            {Element.Water, 2},
            {Element.Electric, 1},
            {Element.Grass, 0.5},
            {Element.Ice, 1},
            {Element.Fighting, 1},
            {Element.Poison, 0.5},
            {Element.Ground, 2},
            {Element.Flying, 0.5},
            {Element.Psychic, 1},
            {Element.Bug, 0.5},
            {Element.Rock, 2},
            {Element.Ghost, 1},
            {Element.Dragon, 0.5}
        };

        public static Dictionary<Element, double> Ice = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 1},
            {Element.Water, 0.5},
            {Element.Electric, 1},
            {Element.Grass, 2},
            {Element.Ice, 0.5},
            {Element.Fighting, 1},
            {Element.Poison, 1},
            {Element.Ground, 2},
            {Element.Flying, 2},
            {Element.Psychic, 1},
            {Element.Bug, 1},
            {Element.Rock, 1},
            {Element.Ghost, 1},
            {Element.Dragon, 2}
        };

        public static Dictionary<Element, double> Fighting = new Dictionary<Element, double>(){
            {Element.Normal, 2},
            {Element.Fire, 1},
            {Element.Water, 1},
            {Element.Electric, 1},
            {Element.Grass, 1},
            {Element.Ice, 2},
            {Element.Fighting, 1},
            {Element.Poison, 0.5},
            {Element.Ground, 1},
            {Element.Flying, 0.5},
            {Element.Psychic, 0.5},
            {Element.Bug, 0.5},
            {Element.Rock, 2},
            {Element.Ghost, 0},
            {Element.Dragon, 1}
        };

        public static Dictionary<Element, double> Poison = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 1},
            {Element.Water, 1},
            {Element.Electric, 1},
            {Element.Grass, 2},
            {Element.Ice, 1},
            {Element.Fighting, 1},
            {Element.Poison, 0.5},
            {Element.Ground, 0.5},
            {Element.Flying, 1},
            {Element.Psychic, 1},
            {Element.Bug, 2},
            {Element.Rock, 0.5},
            {Element.Ghost, 0.5},
            {Element.Dragon, 1}
        };

        public static Dictionary<Element, double> Ground = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 2},
            {Element.Water, 1},
            {Element.Electric, 2},
            {Element.Grass, 0.5},
            {Element.Ice, 1},
            {Element.Fighting, 1},
            {Element.Poison, 2},
            {Element.Ground, 1},
            {Element.Flying, 0},
            {Element.Psychic, 1},
            {Element.Bug, 0.5},
            {Element.Rock, 2},
            {Element.Ghost, 1},
            {Element.Dragon, 1}
        };

        public static Dictionary<Element, double> Flying = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 1},
            {Element.Water, 1},
            {Element.Electric, 0.5},
            {Element.Grass, 2},
            {Element.Ice, 1},
            {Element.Fighting, 2},
            {Element.Poison, 1},
            {Element.Ground, 1},
            {Element.Flying, 1},
            {Element.Psychic, 1},
            {Element.Bug, 2},
            {Element.Rock, 0.5},
            {Element.Ghost, 1},
            {Element.Dragon, 1}
        };

        public static Dictionary<Element, double> Psychic = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 1},
            {Element.Water, 1},
            {Element.Electric, 1},
            {Element.Grass, 1},
            {Element.Ice, 1},
            {Element.Fighting, 2},
            {Element.Poison, 2},
            {Element.Ground, 1},
            {Element.Flying, 1},
            {Element.Psychic, 0.5},
            {Element.Bug, 1},
            {Element.Rock, 1},
            {Element.Ghost, 1},
            {Element.Dragon, 1}
        };

        public static Dictionary<Element, double> Bug = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 0.5},
            {Element.Water, 1},
            {Element.Electric, 1},
            {Element.Grass, 2},
            {Element.Ice, 1},
            {Element.Fighting, 0.5},
            {Element.Poison, 2},
            {Element.Ground, 1},
            {Element.Flying, 0.5},
            {Element.Psychic, 2},
            {Element.Bug, 1},
            {Element.Rock, 1},
            {Element.Ghost, 0.5},
            {Element.Dragon, 1}
        };

        public static Dictionary<Element, double> Rock = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 2},
            {Element.Water, 1},
            {Element.Electric, 1},
            {Element.Grass, 1},
            {Element.Ice, 2},
            {Element.Fighting, 0.5},
            {Element.Poison, 1},
            {Element.Ground, 0.5},
            {Element.Flying, 2},
            {Element.Psychic, 1},
            {Element.Bug, 2},
            {Element.Rock, 1},
            {Element.Ghost, 1},
            {Element.Dragon, 1}
        };

        public static Dictionary<Element, double> Ghost = new Dictionary<Element, double>(){
            {Element.Normal, 0},
            {Element.Fire, 1},
            {Element.Water, 1},
            {Element.Electric, 1},
            {Element.Grass, 1},
            {Element.Ice, 1},
            {Element.Fighting, 1},
            {Element.Poison, 1},
            {Element.Ground, 1},
            {Element.Flying, 0},
            {Element.Psychic, 1},
            {Element.Bug, 1},
            {Element.Rock, 1},
            {Element.Ghost, 2},
            {Element.Dragon, 1}
        };  

        public static Dictionary<Element, double> Dragon = new Dictionary<Element, double>(){
            {Element.Normal, 1},
            {Element.Fire, 1},
            {Element.Water, 1},
            {Element.Electric, 1},
            {Element.Grass, 1},
            {Element.Ice, 1},
            {Element.Fighting, 1},
            {Element.Poison, 1},
            {Element.Ground, 1},
            {Element.Flying, 1},
            {Element.Psychic, 1},
            {Element.Bug, 1},
            {Element.Rock, 1},
            {Element.Ghost, 1},
            {Element.Dragon, 2}
        };

        public static Dictionary<Element, Dictionary<Element, double>> Table = new Dictionary<Element, Dictionary<Element, double>>(){
            {Element.Normal, Normal},
            {Element.Fire, Fire},
            {Element.Water, Water},
            {Element.Electric, Electric},
            {Element.Grass, Grass},
            {Element.Ice, Ice},
            {Element.Fighting, Fighting},
            {Element.Poison, Poison},
            {Element.Ground, Ground},
            {Element.Flying, Flying},
            {Element.Psychic, Psychic},
            {Element.Bug, Bug},
            {Element.Rock, Rock},
            {Element.Ghost, Ghost},
            {Element.Dragon, Dragon}
        };
    }
}