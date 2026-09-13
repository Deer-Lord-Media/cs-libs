using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Quest.Data;

namespace Quest.Tracker
{
    public class Encounter
    {
        public Encounter(Data.Encounter data, IReadOnlyDictionary<Guid, State> database)
        {
            this.data = data;
            this.database = database;
            cached = null;
        }

        private readonly Data.Encounter data;
        private readonly IReadOnlyDictionary<Guid, State> database;
        private Outcome? cached;

        public Outcome? State()
        {
            if (cached != null) return cached;

            foreach (Outcome outcome in data.outcomes)
            {
                var total = outcome.comparators.Values.Count;
                if (total == 0) continue;

                var found = 0;
                foreach (Comparator comparator in outcome.comparators.Values)
                {
                    State state = database[comparator.stateId];
                    if (comparator.Compare(state.Value))
                    {
                        found++;
                    }
                }
                if (found == total)
                {
                    cached = outcome;
                }
            }
            return cached;
        }
    }
}
