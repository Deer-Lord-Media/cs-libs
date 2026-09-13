using System;
using System.Collections.Generic;
using Quest.Data;

namespace Quest.Tracker
{
    public class Encounter
    {
        /*
        An encounter is when you run into something that is going on
        this would be entering a peasant's house, finding a wolf in the woods, etc
        
        an encounter can be in a given state based on events and their values
        as defined by the comparators

        multiple encounters can rely on the same information, which can lead to a more immersive story
        - the peasant has told you about the wolf
        - you see the wolf and don't take care of him
        - the peasant is impatient
        - the peasant handles it himself
        - you find the peasant dead
        this is more varied and immersive than
        - get quest
        - do thing/get thing
        - turn in quest
        it's possible to create many intermediary stages and disjointed encounters
        to create an illusion of a "living" world
        */
        public Encounter(Data.Encounter data, IReadOnlyDictionary<Guid, Event> events, IReadOnlyDictionary<Guid, Knowledge> knowledge)
        {
            this.data = data;
            this.events = events;
            this.knowledge = knowledge;
        }

        private readonly Data.Encounter data;
        private readonly IReadOnlyDictionary<Guid, Event> events;
        private readonly IReadOnlyDictionary<Guid, Knowledge> knowledge;
        public State? State()
        {
            foreach (State state in data.states)
            {
                int total = state.comparators.Values.Count;
                if (total == 0) continue;

                int found = 0;
                foreach (Comparator comparator in state.comparators.Values)
                {
                    if (Compare(comparator)) found++;
                }
                if (found == total)
                {
                    return state;
                }
            }
            return null;
        }

        private bool Compare(Comparator comparator)
        {
            int? value = null;
            switch (comparator.againstType)
            {
                case Against.EVENT:
                    Event ev = events[comparator.againstId];
                    value = ev.Value;
                    break;
                case Against.KNOWLEDGE:
                    Knowledge knwl = knowledge[comparator.againstId];
                    value = knwl.value;
                    break;
                default:
                    break;
            }
            return (value != null) && comparator.Compare((int)value);
        }
    }
}
