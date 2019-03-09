using System.Diagnostics;
using Xamarin.Forms;

namespace Alphabets.Helpers
{
    /// <summary>
    /// A wrapper for Application.Current.Resources (Resource dictionary) which supplies convenience methods to get colors, styles etc.
    /// </summary>
    public static class ResourceDictionary
    {
        /// <summary>
        /// Tries to find a color using a given key.
        /// If no valid color is found, a default value is returned.
        /// </summary>
        public static Color GetColor(string key)
        {
            return Get<Color>(key);
        }

        /// <summary>
        /// Tries to find a style using a given key.
        /// If no valid style is found, a default value is returned.
        /// </summary>
        public static Style GetStyle(string key)
        {
            return Get<Style>(key);
        }

        /// <summary>
        /// Tries to find an object of type T using a given key.
        /// If no valid object is found, a default value is returned.
        /// </summary>
        private static T Get<T>(string key)
        {
            if (TryGetValue(key, out object value))
            {
                if (value is T)
                {
                    return (T)value;
                }
                Debug.WriteLine($"Object with key={key} isn't a {typeof(T)}.");
            }

            Debug.WriteLine($"No object with key={key} exists.");
            return default(T);
        }

        /// <summary>
        /// Tries the get a value assosciated with a key from the resources dictionary.
        /// </summary>
        private static bool TryGetValue(string key, out object value)
        {
            return Application.Current.Resources.TryGetValue(key, out value);
        }
    }
}
