using System.Collections.Generic;
using System.Text;

namespace DeFuncArt.Helpers
{
    /// <summary>
    /// A class of static methods to format collections into strings.
    /// </summary>
    public static class DebugHelper
    {
        /// <summary>
        /// Formats a Dictionary into a string.
        /// </summary>
        public static string DictionaryToString<T1, T2>(Dictionary<T1, T2> dictionary)
        {
            if(dictionary != null)
            {
                //build a string representation of the dictionary
                StringBuilder stringBuilder = new StringBuilder("{");
                bool addComma = false;
                foreach(KeyValuePair<T1, T2> kvp in dictionary)
                {
                    if (addComma) { stringBuilder.Append(", "); }

                    if (!addComma) { addComma = true; }

                    stringBuilder.Append($"{kvp.Key}: {kvp.Value}");
                }
                stringBuilder.Append("}");

                //return the built string
                return stringBuilder.ToString();
            }

            //otherwise return null
            return null;
        }
    }
}
