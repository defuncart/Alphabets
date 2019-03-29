using System.Collections;
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
        /// Formats an Array into a string.
        /// </summary>
        public static string ArrayToString<T>(T[] array)
        {
            if (array != null)
            {
                //build a string representation of the dictionary
                StringBuilder stringBuilder = new StringBuilder("[");
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 0) { stringBuilder.Append(", "); }

                    stringBuilder.Append(array[i].ToString());
                }
                stringBuilder.Append("]");

                //return the built string
                return stringBuilder.ToString();
            }

            //otherwise return null
            return null;
        }

        /// <summary>
        /// Formats an IEnumerable into a string.
        /// </summary>
        public static string EnumerableToString<T>(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                //build a string representation of the dictionary
                StringBuilder stringBuilder = new StringBuilder("[");
                bool addComma = false;
                foreach (IEnumerable enumerable in collection)
                {
                    if (addComma) { stringBuilder.Append(", "); }

                    if (!addComma) { addComma = true; }

                    stringBuilder.Append(enumerable.ToString());
                }
                stringBuilder.Append("]");

                //return the built string
                return stringBuilder.ToString();
            }

            //otherwise return null
            return null;
        }

        /// <summary>
        /// Formats a Dictionary into a string.
        /// </summary>
        public static string DictionaryToString<T1, T2>(Dictionary<T1, T2> dictionary)
        {
            if (dictionary != null)
            {
                //build a string representation of the dictionary
                StringBuilder stringBuilder = new StringBuilder("{");
                bool addComma = false;
                foreach (KeyValuePair<T1, T2> kvp in dictionary)
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
