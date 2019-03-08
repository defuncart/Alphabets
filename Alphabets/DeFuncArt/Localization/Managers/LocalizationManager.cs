using DeFuncArt.Localization.Helpers;
using Plugin.Multilingual;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace DeFuncArt.Localization.Managers
{
    /// <summary>
    /// A localization manager which acts as a wrapper for the multilingual plugin.
    /// </summary>
    public static class LocalizationManager
    {
        #region Constants

        //TODO
        /// <summary>A resource identifier (full path) to Localization resx.</summary>
        private const string RESOURCE_ID = "Alphabets.Resources.Localization.Resources";

        #endregion

        #region Variables

        /// <summary>The resource manager.</summary>
        private static readonly Lazy<ResourceManager> resmgr = new Lazy<ResourceManager>(() => new ResourceManager(RESOURCE_ID, typeof(LocalizationManager).GetTypeInfo().Assembly));

        #endregion

        #region Properties

        /// <summary>
        /// The device's culture info.
        /// </summary>
        public static CultureInfo DeviceCultureInfo => CrossMultilingual.Current.DeviceCultureInfo;

        /// <summary>
        /// The current culture info.
        /// </summary>
        public static CultureInfo CurrentCultureInfo => CrossMultilingual.Current.CurrentCultureInfo;

        #endregion

        #region Methods

        /// <summary>
        /// Sets the langauge.
        /// </summary>
        /// <param name="requestedLangauge">Requested langauge.</param>
        /// <param name="defaultLangauge">Default langauge.</param>
        public static void SetLangauge(string requestedLangauge, string defaultLangauge = "en")
        {
            //determine language to load
            string language = requestedLangauge;
            if (!LocalizationHelper.IsValidLanguage(requestedLangauge))
            {
                language = defaultLangauge;
            }
            CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.NeutralCultureInfoList.ToList().First(element => element.TwoLetterISOLanguageName == language);

            //TODO
            Alphabets.Resources.Localization.Resources.Culture = LocalizationManager.CurrentCultureInfo;
        }

        /// <summary>
        /// Returns a localized value for a given key. If there is no key-value pair, the key is returned.
        /// </summary>
        public static string GetValue(string key)
        {
            //try to get the translation from the ResourceManager
            string translation = resmgr.Value.GetString(key, CrossMultilingual.Current.CurrentCultureInfo);

            //if the translation is null, set the key to be the translation itself
            if (translation == null)
            {
                translation = key;
            }

            //return the translation
            return translation;
        }

        #endregion
    }
}
