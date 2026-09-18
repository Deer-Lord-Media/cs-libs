using System.Linq;
using World.Data;

using Events = System.Collections.Generic.IReadOnlyDictionary<System.Guid, World.Tracker.Event>;
using Information = System.Collections.Generic.IReadOnlyDictionary<System.Guid, World.Tracker.Knowledge>;


namespace World.Tracker
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
        public Encounter(Data.Encounter data, Events events, Information knowledge)
        {
            this.data = data;
            this.events = events;
            this.knowledge = knowledge;
        }

        private readonly Data.Encounter data;
        private readonly Events events;
        private readonly Information knowledge;

        private State? state;
        public State? State
        {
            get
            {
                if (state != null) return state;

                foreach (State state in data.states)
                {
                    int total = state.comparators.Values.Count();
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
            int? value = comparator.againstType switch
            {
                Against.EVENT => events[comparator.againstId].Value,
                Against.KNOWLEDGE => knowledge[comparator.againstId].Flags,
                _ => null,
            };
            return (value != null) && comparator.Compare((int)value);
        }
    }
}
