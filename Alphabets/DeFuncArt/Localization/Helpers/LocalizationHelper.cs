using System;

namespace DeFuncArt.Localization.Helpers
{
    /// <summary>
    /// A helper class for localization.
    /// </summary>
    public static class LocalizationHelper
    {
        /// <summary>
        /// An array of valid languages.
        /// </summary>
        private static readonly string[] VALID_LANGUAGES = { "en", "de", "pl" };

        /// <summary>
        /// Determines whether a given language is valid for this project.
        /// </summary>
        public static bool IsValidLanguage(string language)
        {
            return Array.IndexOf(VALID_LANGUAGES, language) != -1;
        }
    }
}
