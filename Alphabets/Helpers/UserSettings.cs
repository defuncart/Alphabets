using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Alphabets.Helpers
{
    /// <summary>
    /// A <see langword="static"/> class which can be used to save user settings.
    /// Uses Xam.Plugins.Settings.
    /// </summary>
    public static class UserSettings
    {
        /// <summary>
        /// Gets the app settings.
        /// </summary>
        /// <value>The app settings.</value>
        private static ISettings AppSettings => CrossSettings.Current;

        /// <summary>
        /// A struct of lookup keys for settings.
        /// </summary>
        private struct Keys
        {
            /// <summary>The langauge key.</summary>
            public const string LANGUAGE = "language";
            /// <summary>The alreadyLaunched key.</summary>
            public const string ALREADY_LAUNCHED = "alreadyLaunched";
        }

        /// <summary>
        /// The display language.
        /// </summary>
        public static string Language
        {
            get => AppSettings.GetValueOrDefault(Keys.LANGUAGE, "en");
            set => AppSettings.AddOrUpdateValue(Keys.LANGUAGE, value);
        }

        /// <summary>
        /// Whether it is the first launch (of the app).
        /// </summary>
        public static bool AlreadyLaunched
        {
            get => AppSettings.GetValueOrDefault(Keys.ALREADY_LAUNCHED, false);
            set => AppSettings.AddOrUpdateValue(Keys.ALREADY_LAUNCHED, value);
        }

        /// <summary>
        /// Clears all saved preferences.
        /// </summary>
        public static void Clear()
        {
            AppSettings.Clear();
        }
    }
}
