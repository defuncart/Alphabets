using System.Collections.Generic;
using System.Linq;

namespace DeFuncArt.Extensions
{
    /// <summary>A collection of extention methods.</summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Shuffles the enumerable.
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(x => random.Next()).ToList();
        }
    }
}
