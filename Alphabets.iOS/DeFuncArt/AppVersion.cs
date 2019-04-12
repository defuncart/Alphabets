using DeFuncArt.PlatformSpecific;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(DeFuncArt.iOS.AppVersion))]
namespace DeFuncArt.iOS
{
    /// <summary>
    /// iOS implementation of IAppVersion to retrieve the app's version (x.x.xx) and build number (xxx).
    /// </summary>
    public class AppVersion : IAppVersion
    {
        /// <summary>
        /// Gets the app version (of the form x.x.xx)
        /// </summary>
        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }

        /// <summary>
        /// Gets the app build number (iOS) / version code (Android) (of the form xxx).
        /// </summary>
        public int GetBuildNumber()
        {
            return int.Parse(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString());
        }
    }
}