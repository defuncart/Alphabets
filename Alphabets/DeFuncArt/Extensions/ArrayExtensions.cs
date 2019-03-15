using System;

namespace DeFuncArt.Extensions
{
    /// <summary>A collection of array extention methods.</summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Determines whether an array contains a given value.
        /// </summary>
        public static bool Contains<T>(this T[] array, T value)
        {
            return array.IndexOf<T>(value) >= 0; //if index is >= zero, then array contains object
        }

        /// <summary>
        /// Returns the index of a given value in a given array. 
        /// Returns -1 if the value isn't in the array.
        /// </summary>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        /// <summary>
        /// Changes every element in an array to a given value.
        /// </summary>
        public static T[] Repeat<T>(this T[] array, T value)
        {
            if (array == null) { return null; }
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
            return array;
        }
    }
}
