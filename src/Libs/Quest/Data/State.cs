using System;
using System.Collections.Generic;

namespace Quest.Data
{
    /// <summary>
    /// Represents a possible state within an <see cref="Encounter"/>, defined by a set of
    /// comparators evaluated to determine an outcome.
    /// </summary>
    public readonly struct State
    {
        /// <summary>
        /// The identifier of the <see cref="Encounter"/> this state belongs to.
        /// </summary>
        public readonly Guid encounterId;

        /// <summary>
        /// The set of comparators used to evaluate this state, keyed by identifier.
        /// </summary>
        public readonly IReadOnlyDictionary<Guid, Comparator> comparators;

        /// <summary>
        /// The result of evaluating this state's comparators.
        /// </summary>
        public readonly bool outcome;

        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> struct.
        /// </summary>
        /// <param name="encounterId">The identifier of the <see cref="Encounter"/> this state belongs to.</param>
        /// <param name="comparators">The set of comparators used to evaluate this state, keyed by identifier.</param>
        /// <param name="outcome">The result of evaluating this state's comparators.</param>
        public State(Guid encounterId, IReadOnlyDictionary<Guid, Comparator> comparators, bool outcome)
        {
            this.encounterId = encounterId;
            this.comparators = comparators;
            this.outcome = outcome;
        }
    }
}
