using System;
using System.Collections.Generic;
using Quest.Data;

namespace Quest.Tracker
{
    public class Encounter
    {
        // An encounter is when you run into something that is going on
        // this would be entering a peasant's house, finding a wolf in the woods, etc
        // an encounter can be in a given state based on events and their values
        // as defined by the comparators
        public Encounter(Data.Encounter data, IReadOnlyDictionary<Guid, Event> database)
        {
            this.data = data;
            this.database = database;
        }

        private readonly Data.Encounter data;
        private readonly IReadOnlyDictionary<Guid, Event> database;
        public State? State()
        {
            foreach (State outcome in data.states)
            {
                int total = outcome.comparators.Values.Count;
                if (total == 0) continue;

                int found = 0;
                foreach (Comparator comparator in outcome.comparators.Values)
                {
                    Event state = database[comparator.stateId];
                    if (comparator.Compare(state.Value))
                    {
                        found++;
                    }
                }
                if (found == total)
                {
                    return outcome;
                }
            }
            return null;
        }
    }
}
