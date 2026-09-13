using Quest.Data;

using Events = System.Collections.Generic.IReadOnlyDictionary<System.Guid, Quest.Tracker.Event>;
using Knowledge = System.Collections.Generic.IReadOnlyDictionary<System.Guid, Quest.Data.Knowledge>;


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
        public Encounter(Data.Encounter data, Events events, Knowledge knowledge)
        {
            this.data = data;
            this.events = events;
            this.knowledge = knowledge;
        }

        private readonly Data.Encounter data;
        private readonly Events events;
        private readonly Knowledge knowledge;

        private State? state;
        public State? State
        {
            get
            {
                if (state != null) return state;

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
                        if (state.outcome)
                        {
                            this.state = state;
                        }
                        return state;
                    }
                }

                return null;
            }
        }

        private bool Compare(Comparator comparator)
        {
            int? value = null;
            switch (comparator.againstType)
            {
                case Against.EVENT:
                    value = events[comparator.againstId].Value;
                    break;
                case Against.KNOWLEDGE:
                    value = knowledge[comparator.againstId].value;
                    break;
                default:
                    break;
            }
            return (value != null) && comparator.Compare((int)value);
        }
    }
}
