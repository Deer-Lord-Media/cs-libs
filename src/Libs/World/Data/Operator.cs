using System;

namespace World.Data
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
        /// Determines whether <paramref name="o"/> is strictly greater than <paramref name="cmp"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="cmp">The threshold value.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &gt; <paramref name="cmp"/>; otherwise <c>false</c>.</returns>
        private static bool GreaterThan(int t, int cmp)
        {
            return t > cmp;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is strictly less than <paramref name="cmp"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="cmp">The threshold value.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &lt; <paramref name="cmp"/>; otherwise <c>false</c>.</returns>
        private static bool LessThan(int t, int cmp)
        {
            return t < cmp;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is greater than or equal to <paramref name="cmp"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="cmp">The minimum threshold value.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &gt;= <paramref name="cmp"/>; otherwise <c>false</c>.</returns>
        private static bool AtLeast(int t, int cmp)
        {
            return t >= cmp;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is less than or equal to <paramref name="cmp"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="cmp">The maximum threshold value.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &lt;= <paramref name="cmp"/>; otherwise <c>false</c>.</returns>
        private static bool OrLess(int t, int cmp)
        {
            return t <= cmp;
        }

        /// <summary>
        /// Determines whether <paramref name="cmp"/> and <paramref name="t"/> are equal.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="cmp">The reference value.</param>
        /// <returns><c>true</c> if the two values are equal; otherwise <c>false</c>.</returns>
        private static bool Equal(int t, int cmp)
        {
            return t == cmp;
        }

        /// <summary>
        /// Determines whether all of the bits set in <paramref name="flags"/> are also set in <paramref name="t"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="flags">A mask identifying the bits that must all be set.</param>
        /// <returns><c>true</c> if every bit set in <paramref name="flags"/> is also set in <paramref name="t"/>; otherwise <c>false</c>.</returns>
        private static bool AreSet(int t, int flags)
        {
            return (t & flags) == flags;
        }

        /// <summary>
        /// Determines whether none of the bits set in <paramref name="flags"/> are set in <paramref name="t"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="flags">A mask identifying the bits that must all be clear.</param>
        /// <returns><c>true</c> if every bit set in <paramref name="flags"/> is clear in <paramref name="t"/>; otherwise <c>false</c>.</returns>
        private static bool NotSet(int t, int flags)
        {
            return (t & flags) == 0;
        }

        /// <summary>
        /// Determines whether any of the bits set in <paramref name="flags"/> are also set in <paramref name="t"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="flags">A mask identifying the bits of interest.</param>
        /// <returns><c>true</c> if at least one bit set in <paramref name="flags"/> is also set in <paramref name="t"/>; otherwise <c>false</c>.</returns>
        private static bool AnyOf(int t, int flags)
        {
            return (t & flags) != 0;
        }
    }
}
