using System;

namespace Quest.Data
{
    /// <summary>
    /// Provides comparison operator functions that can be resolved dynamically
    /// from a <see cref="Symbol"/> value. Used by <see cref="Comparator"/> to
    /// evaluate integer comparisons based on a stored operator symbol.
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// Resolves the comparison function associated with the given <see cref="Symbol"/>.
        /// </summary>
        /// <param name="symbol">The symbol identifying which comparison operator to use.</param>
        /// <returns>
        /// A <see cref="Func{T1, T2, TResult}"/> that takes a reference value and an
        /// input value (in that order) and returns whether the comparison holds;
        /// or <c>null</c> if <paramref name="symbol"/> does not match a known operator.
        /// </returns>
        public static Func<int, int, bool>? Function(Symbol symbol)
        {
            Func<int, int, bool>? op = symbol switch
            {
                Symbol.GREATER_THAN => GreaterThan,
                Symbol.LESS_THAN => LessThan,
                Symbol.AT_LEAST => AtLeast,
                Symbol.OR_LESS => OrLess,
                Symbol.EQUAL => Equal,
                Symbol.ARE_SET => AreSet,
                Symbol.NOT_SET => NotSet,
                Symbol.ANY_OF => AnyOf,
                _ => null,
            };
            return op;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is strictly greater than <paramref name="gt"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="gt">The threshold value.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &gt; <paramref name="gt"/>; otherwise <c>false</c>.</returns>
        private static bool GreaterThan(int t, int gt)
        {
            return t > gt;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is strictly less than <paramref name="lt"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="lt">The threshold value.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &lt; <paramref name="lt"/>; otherwise <c>false</c>.</returns>
        private static bool LessThan(int t, int lt)
        {
            return t < lt;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is greater than or equal to <paramref name="al"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="al">The minimum threshold value.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &gt;= <paramref name="al"/>; otherwise <c>false</c>.</returns>
        private static bool AtLeast(int t, int al)
        {
            return t >= al;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is less than or equal to <paramref name="ol"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="ol">The maximum threshold value.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &lt;= <paramref name="ol"/>; otherwise <c>false</c>.</returns>
        private static bool OrLess(int t, int ol)
        {
            return t <= ol;
        }

        /// <summary>
        /// Determines whether <paramref name="eq"/> and <paramref name="t"/> are equal.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="eq">The reference value.</param>
        /// <returns><c>true</c> if the two values are equal; otherwise <c>false</c>.</returns>
        private static bool Equal(int t, int eq)
        {
            return t == eq;
        }

        /// <summary>
        /// Determines whether all of the bits set in <paramref name="and"/> are also set in <paramref name="t"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="and">A mask identifying the bits that must all be set.</param>
        /// <returns><c>true</c> if every bit set in <paramref name="and"/> is also set in <paramref name="t"/>; otherwise <c>false</c>.</returns>
        private static bool AreSet(int t, int and)
        {
            return (t & and) == and;
        }

        /// <summary>
        /// Determines whether none of the bits set in <paramref name="and"/> are set in <paramref name="t"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="and">A mask identifying the bits that must all be clear.</param>
        /// <returns><c>true</c> if every bit set in <paramref name="and"/> is clear in <paramref name="t"/>; otherwise <c>false</c>.</returns>
        private static bool NotSet(int t, int and)
        {
            return (t & and) == 0;
        }

        /// <summary>
        /// Determines whether any of the bits set in <paramref name="and"/> are also set in <paramref name="t"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="and">A mask identifying the bits of interest.</param>
        /// <returns><c>true</c> if at least one bit set in <paramref name="and"/> is also set in <paramref name="t"/>; otherwise <c>false</c>.</returns>
        private static bool AnyOf(int t, int and)
        {
            return (t & and) != 0;
        }
    }
}
