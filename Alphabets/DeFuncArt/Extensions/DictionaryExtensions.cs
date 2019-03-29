using System.Collections.Generic;
using System.Linq;

namespace DeFuncArt.Extensions
{
    /// <summary>A collection of extention methods.</summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Determines whether an array contains a given value.
        /// </summary>
        public static KeyValuePair<T1, T2> RandomEntry<T1, T2>(this Dictionary<T1, T2> dictionary)
        {
            return dictionary.ElementAt(random.Next(dictionary.Count));
        }
    }
}
