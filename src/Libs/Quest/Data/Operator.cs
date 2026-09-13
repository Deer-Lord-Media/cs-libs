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
            Func<int, int, bool>? op = null;
            switch (symbol)
            {
                case Symbol.GREATER_THAN:
                    op = GreaterThan;
                    break;
                case Symbol.LESS_THAN:
                    op = LessThan;
                    break;
                case Symbol.AT_LEAST:
                    op = AtLeast;
                    break;
                case Symbol.OR_LESS:
                    op = OrLess;
                    break;
                case Symbol.EQUAL:
                    op = Equal;
                    break;
                case Symbol.ARE_SET:
                    op = AreSet;
                    break;
                case Symbol.NOT_SET:
                    op = NotSet;
                    break;
            }
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
        /// Determines whether every bit set in <paramref name="and"/> is also set in <paramref name="t"/>.
        /// </summary>
        /// <param name="t">The value being tested.</param>
        /// <param name="and">The mask of bits that must all be present in <paramref name="t"/>.</param>
        /// <returns>
        /// <c>true</c> if <c>t &amp; and</c> equals <paramref name="and"/> (i.e. <paramref name="t"/>
        /// has every bit set that <paramref name="and"/> has set); otherwise <c>false</c>.
        /// </returns>
        private static bool AreSet(int t, int and)
        {
            return (t & and) == and;
        }

        private static bool NotSet(int t, int and)
        {
            return (t & and) == 0;
        }
    }
}

// use OR for adding flags: